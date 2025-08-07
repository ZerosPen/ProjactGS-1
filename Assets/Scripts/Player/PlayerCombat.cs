using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : Player
{
    [Header("Status")]
    private List<GameObject> fireballContainer = new List<GameObject>();
    public GameObject chargeVFX;
    private GameObject activeChargeVFX;
    public float coolDownTime;
    private float currCoolDown;
    private float lastDirection = 1;
    public KeyCode attackKey = KeyCode.R;

    public GameObject fireBallPrefabs;
    private NewPlayerMove movement;

    private void Start()
    {
        movement = GetComponent<NewPlayerMove>();

        if (fireBallPrefabs == null)
            Debug.LogError("FireBall prefab not assigned!");

        if (movement == null)
            Debug.LogError("PlayerMovement script not found!");
    }

    private void Update()
    {
        if (movement.movementInput.x != 0)
        {
            lastDirection = movement.movementInput.x;
        }

        if (currCoolDown > 0)
        {
            currCoolDown -= Time.deltaTime;
        }

        if (Input.GetKey(attackKey) && currCoolDown <= 0)
        {
            if (activeChargeVFX == null)
            {
                activeChargeVFX = Instantiate(chargeVFX, transform.position, Quaternion.identity);
                activeChargeVFX.transform.SetParent(transform);
            }
        }
    }

    public void FireBalls(InputAction.CallbackContext contex)
    {
        if (contex.canceled && currCoolDown <= 0)
        {
            if (activeChargeVFX != null)
            {
                Destroy(activeChargeVFX);
                activeChargeVFX = null;
            }
            SpawnFireBall();
            currCoolDown = coolDownTime;
        }
    }

    public void SpawnFireBall()
    {
        for (int i = 0; i < fireballContainer.Count; i++)
        {
            if (!fireballContainer[i].activeInHierarchy)
            {
                fireballContainer[i].transform.position = transform.position;
                fireballContainer[i].SetActive(true);
                fireballContainer[i].GetComponent<FireBall>().direction = lastDirection;
                return;
            }
        }

        GameObject newFireball = Instantiate(fireBallPrefabs, transform.position, Quaternion.identity);
        newFireball.GetComponent<FireBall>().direction = lastDirection;
        fireballContainer.Add(newFireball);
    }

    private void ChargeEffect(Vector3 position)
    {
        GameObject effect = Instantiate(chargeVFX, position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}

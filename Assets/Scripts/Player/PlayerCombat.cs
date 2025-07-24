using System.Collections.Generic;
using UnityEngine;

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
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();

        if (fireBallPrefabs == null)
            Debug.LogError("FireBall prefab not assigned!");

        if (movement == null)
            Debug.LogError("PlayerMovement script not found!");
    }

    private void Update()
    {
        float direction = movement.direction;
        if (direction != 0)
        {
            lastDirection = direction;
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

        if (Input.GetKeyUp(attackKey) && currCoolDown <= 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity, Idamagetable
{
    [Header("Status")]
    public float maxHealthPoint;

    [Header("References")]
    public GameObject PlayerPos;
    public Slider healthBar;
    private FloatingStatus statusBar;
    public float healthPoint { get; set; }
    public float distancePlayer;
    public Vector2 distanceTarget;

    private void Start()
    {
        //isRoaming = true;
        healthPoint = maxHealthPoint;
    }

    public void TakeDamage (float damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Update ()
    {
        healthBar.value = healthPoint / maxHealthPoint;
        distancePlayer = Vector2.Distance(transform.position, PlayerPos.transform.position);
        Debug.Log("Distance to Player: " + distancePlayer);
    }
}

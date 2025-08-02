using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity, Idamagetable
{
    [Header("Status")]
    public float maxHealthPoint;
    public bool isRoaming;
    public bool isChasing;
    public bool canJump;

    [Header("References")]
    public GameObject PlayerPos;
    public GameObject FireballPos;
    public Image healthBar;
    private FloatingStatus statusBar;
    public float healthPoint { get; set; }
    public float distancePlayer;
    public float distanceTarget;

    private void Start()
    {
        isRoaming = true;
        healthPoint = maxHealthPoint;
        statusBar = GetComponentInChildren<FloatingStatus>();
    }

    public void TakeDamage (float damage)
    {
        healthPoint -= damage;
        statusBar.UpdateStatus(healthPoint, maxHealthPoint);
        if (healthPoint <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Update ()
    {
        healthBar.fillAmount = healthPoint / maxHealthPoint;   
        distancePlayer = (transform.position.x - PlayerPos.transform.position.x);
        distanceTarget = (transform.position.x - FireballPos.transform.position.x);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Entity, Idamagetable
{
    [Header("Status")]
    public float maxHealthPoint;

    [Header("References")]
    public Image healthBar;
    private FloatingStatus statusBar;
    public float healthPoint { get; set; }

    private void Start()
    {
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
    }
}

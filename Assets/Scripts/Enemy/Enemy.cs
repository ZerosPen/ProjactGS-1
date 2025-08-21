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
    public float distanceTarget;

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
        distancePlayer = Mathf.Abs((transform.position.x - PlayerPos.transform.position.x));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity, Idamagetable
{
    [Header("Stats")]
    public float maxHealthpoint;

    [Header("References")]
    public Slider healthbar;
    public float healthPoint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        healthPoint = maxHealthpoint;
    }

    public void TakeDamage (float damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            gameObject.SetActive (false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = healthPoint / maxHealthpoint;
    }
}

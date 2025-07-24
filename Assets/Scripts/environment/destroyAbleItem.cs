using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAbleItem : Entity, Idamagetable
{
    [Header("Status")]
    public float maxHealthPoint;

    public float healthPoint { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        healthPoint = maxHealthPoint;
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
        Debug.Log("Current health : " + healthPoint);
    }
}

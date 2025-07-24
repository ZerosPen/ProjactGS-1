using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingStatus : MonoBehaviour
{
    [SerializeField] private Slider slider;
    

    public void UpdateStatus(float health, float maxHealth)
    {
        slider.value = health /  maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

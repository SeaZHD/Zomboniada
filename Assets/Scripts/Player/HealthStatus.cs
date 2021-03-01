using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    public Slider slider;
    public GameObject healthBarUI;

    public float maxHP = 100;
    public float currentHP;

    void Start()
    {
        
        currentHP = maxHP;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();

        if (currentHP < maxHP)
            healthBarUI.SetActive(true);
    }


    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
    }

    float CalculateHealth()
    {
        return currentHP / maxHP;
    }

}

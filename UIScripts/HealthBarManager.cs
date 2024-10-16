using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    public Slider slider;
    
    void Start()
    {
        slider.maxValue = owner.GetComponent<HealthController>().maxHealth;
    }


    void Update()
    {
        if(owner != null)
            slider.value = owner.GetComponent<HealthController>().health;
    }
}

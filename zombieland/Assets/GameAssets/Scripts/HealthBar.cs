 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider heathBarSlider;

    public void giveFullHealth(float health)
    {
        heathBarSlider.maxValue = health;
        heathBarSlider.value = health;
    }

    public void SetHealth(float health)
    {
        heathBarSlider.value = health;
    }
}

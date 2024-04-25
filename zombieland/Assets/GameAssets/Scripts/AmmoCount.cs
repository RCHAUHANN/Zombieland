using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammunationText;
    public TextMeshProUGUI magText;

    public static AmmoCount occurance;

    private void Awake()
    {
        occurance = this;

    }

    public void UpateAmmoText(int presentAmmo)
    {
        ammunationText.text = "Ammo. " + presentAmmo;
    }

    public void UpdateMagText(int mag)
    {
        magText.text = "Magzines. " + mag;
    }
}

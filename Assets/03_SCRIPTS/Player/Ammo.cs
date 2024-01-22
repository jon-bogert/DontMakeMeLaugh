using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public float _ammo;
    public float _clip;  


    [SerializeField] float _clipMax;    
    [SerializeField] float _ammoMax;
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] Slider _clipSlider;
    
    public void Fire()
    {
        _clip -= 1;
        UpdateUI();
    }

    public void Reload()
    {
        if (_clip != 0)
        {
            _ammo += _clip;
            _clip = 0;
        }
        _ammo -= _clipMax;
        _clip += _clipMax;
        UpdateUI();
    }

    public void AmmoPickup(float pickupAmount)
    {
        if (_ammo < _ammoMax)
        {
            _ammo += pickupAmount;
        }
        else
        {
            Debug.Log("Ammo Maxed");
        }
       
    }

    public void UpdateUI()
    {
        _ammoText.text = _ammo.ToString();
        _clipSlider.value = _clip;
    }
    
}

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

    private void Update()
    {
        if (_clip == 0)
        {
            Reload();
        }

        //Ammo Testing
        bool mouseInput = Input.GetMouseButtonDown(0);
        if (mouseInput)
        {
            Fire();
        }
        bool reloadInput = Input.GetKeyDown("r");
        if (reloadInput)
        {
            Reload();
        }
    }

    public void Fire()
    {
        _clip -= 1;
        UpdateUI();
    }

    public void Reload()
    {
        if (_ammo != 0)
        {
            if (_clip != 0)
            {
                _ammo += _clip;
                _clip = 0;
            }
            _ammo -= _clipMax;
            _ammo = Mathf.Clamp(_ammo, 0, _ammoMax);
            _clip += _clipMax;
            _clip = Mathf.Clamp(_clip, 0, _clipMax);
            UpdateUI();
        }
        else
        {
            Debug.Log("Out of Ammo!");
        }
        
    }

    //Move to ammo pickup script?
    public void AmmoPickup(float pickupAmount)
    {
        if (_ammo < _ammoMax)
        {
            _ammo += pickupAmount;
            _ammo = Mathf.Clamp(_ammo, 0, _ammoMax);
            UpdateUI();
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

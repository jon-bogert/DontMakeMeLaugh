using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float _health;

    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] float _healthMax;    

    private void Start()
    {
        if (_healthText != null)
        {
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(gameObject.name + ": has taken damage");
        _health -= damage;
        _health = Mathf.Clamp(_health, -100, _healthMax);
        if (_health <= 0)
        {
            Debug.Log(gameObject.name + ": has died");
            Destroy(gameObject);
        }
        if (_healthText != null)
        {
            UpdateUI();
        }
        
    }
    public void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }
   
}

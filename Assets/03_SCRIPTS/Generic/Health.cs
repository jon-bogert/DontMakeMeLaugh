using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] float _healthMax;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _healthText;

    [Header("Events")]
    [SerializeField] UnityEvent _onDeath;
    [SerializeField] UnityEvent _onDamaged;

    public float health { get { return _health; } }

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
        _onDamaged?.Invoke();
        _health -= damage;
        _health = Mathf.Clamp(_health, -100, _healthMax);
        if (_health <= 0)
        {
            Debug.Log(gameObject.name + ": has died");
            _onDeath?.Invoke();
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

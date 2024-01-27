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
    public UnityEvent onDeath;
    [SerializeField] UnityEvent _onDamaged;

    private float timer;
    private float deathTimer = 2;
    private bool dead = false;

    public float health { get { return _health; } }

    private void Start()
    {
        timer = deathTimer;
        if (_healthText != null)
        {
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        //If it's player health and GODMODE is on, take no damage
        if (GODMODE.isGodModeEnabled && GetComponent<PlayerMovement>() != null)
        {
            return;
        }

        if (dead)
        {
            onDeath.Invoke();
            return;
        }

        Debug.Log(gameObject.name + ": has taken damage");
        _onDamaged?.Invoke();
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _healthMax);
        if (_health <= 0)
        {
            Debug.Log(gameObject.name + ": has died");
            onDeath?.Invoke();
            dead = true;
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
   
    public void Heal(float amount)
    {
        if (_health >= _healthMax)
            return;

        //TODO Play Sound
            _health += amount;
        if (_health > _healthMax)
            _health = _healthMax;

        if (_healthText != null)
        {
            UpdateUI();
        }
    }
}

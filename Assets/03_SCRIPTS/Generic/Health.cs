using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] float _healthMax;

    [Header("References")]
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] SoundPlayer _soundPlayer;
    [SerializeField] AudioSource _musicPlayer;

    [Header("Events")]
    public UnityEvent onDeath;
    [SerializeField] UnityEvent _onDamaged;

    private float timer;
    private float deathTimer = 2;
    private bool dead = false;
    bool _comedian = false;
    bool _player = false;
    float _previousHealth;
    EnemyShoot _comedianScript;
    Color _healthTextColor;

    public float health { get { return _health; } }

    private void Start()
    {
        timer = deathTimer;
        if (TryGetComponent<EnemyShoot>(out EnemyShoot enemyShoot))
        {
            _comedian = true;
            _comedianScript = enemyShoot;
        }
        if (_healthText != null)
        {
            UpdateUI();
            _player = true;
            _healthTextColor = _healthText.color;
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

        _onDamaged?.Invoke();
        _health -= damage;
        if (!_comedian)
        {
            _soundPlayer.Play("hit", SoundPlayer.Bank.Multi);
        }
        else if (_comedian)
        {
            bool setupSaid = _comedianScript.setupSaid;
            if (setupSaid)
            {
                _soundPlayer.Play("hit", SoundPlayer.Bank.Multi);
            }
        }
        
        _health = Mathf.Clamp(_health, 0, _healthMax);
        if (_health <= 0)
        {
            onDeath?.Invoke();
            dead = true;
            if (_player)
            {
                _soundPlayer.Play("deathLaugh", SoundPlayer.Bank.Single);
                _musicPlayer.Pause();
            }
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
            _soundPlayer.Play("heal", SoundPlayer.Bank.Multi);
            _healthText.color = Color.green;
            StartCoroutine(ChangeColorAfterTime(1));
        }
    }
    IEnumerator ChangeColorAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _healthText.color = _healthTextColor;
    }
}

using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] float _damageAmount = 10;
    [Header("Freqency of attacks is found in the EnemyMelee Component")]

    Health _playerHealth = null;
    ZombieAttackSounds _attackSound;
    EnemyMoveSounds _moveSounds;

    private void Awake()
    {
        _attackSound = GetComponent<ZombieAttackSounds>();
        _moveSounds = GetComponentInParent<EnemyMoveSounds>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null)
            return;
        _moveSounds.attacking = true;
        _playerHealth = health;
    }

    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == _playerHealth)
            _playerHealth = null;
        _moveSounds.attacking = false;
    }

    public void Attack()
    {
        if (_playerHealth == null)
            return;

        _attackSound.AttackSound();
        _playerHealth.TakeDamage(_damageAmount);
    }
}

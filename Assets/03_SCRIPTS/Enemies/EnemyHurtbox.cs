using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] float _damageAmount = 10;
    [Header("Freqency of attacks is found in the EnemyMelee Component")]

    Health _playerHealth = null;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == null)
            return;

        _playerHealth = health;
    }

    private void OnTriggerExit(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health == _playerHealth)
            _playerHealth = null;
    }

    public void Attack()
    {
        if (_playerHealth == null)
            return;

        _playerHealth.TakeDamage(_damageAmount);
    }
}

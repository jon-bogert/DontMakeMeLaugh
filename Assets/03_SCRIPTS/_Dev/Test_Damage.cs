using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class Test_Damage : MonoBehaviour
{
    [SerializeField] float _damage;
    [SerializeField] string _playerTag;

    SphereCollider _collider;
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {      
        if (other.CompareTag(_playerTag))
        {
            if (other.TryGetComponent<Health>(out var _playerHealth))
            {
                _playerHealth.TakeDamage(_damage);

                //Enemy Damage Testing
                Health _enemyHealth = GetComponent<Health>();
                _enemyHealth.TakeDamage(5);
            }
            else
            {
                Debug.LogError("A Player tagged game object w/o a health script has hit: " + gameObject.name);
            }

        }
        else
        {
            Debug.Log("Something not a player hit: " + gameObject.name);
        }
    }
}

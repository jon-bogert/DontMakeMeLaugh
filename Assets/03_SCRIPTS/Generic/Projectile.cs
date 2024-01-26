using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float _damageAmount = 10f;
    [SerializeField] float _maxLifetime = 10f;

    float _lifeTimer = 0f;
    Rigidbody _rigidbody;

    public bool isActive { get { return gameObject.activeSelf; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Health health = collision.transform.GetComponent<Health>();
        if (health == null)
        {
            gameObject.SetActive(false);
            return;
        }

        health.TakeDamage(_damageAmount);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0 )
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void Activate(Vector3 position, Vector3 velocity)
    {
        gameObject.SetActive(true);
        _lifeTimer = _maxLifetime;
        _rigidbody.position = position;
        _rigidbody.velocity = velocity;
    }
}

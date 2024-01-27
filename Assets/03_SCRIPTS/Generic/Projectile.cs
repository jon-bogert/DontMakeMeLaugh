using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(SphereCollider))]
[RequireComponent (typeof(MeshRenderer))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float _damageAmount = 10f;
    [SerializeField] float _maxLifetime = 10f;

    float _lifeTimer = 0f;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;
    SoundPlayer _soundPlayer;

    public bool isActive { get { return gameObject.activeSelf; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }  

    private void OnTriggerEnter(Collider collision)
    {
        Health health = collision.transform.GetComponent<Health>();
        if (health == null)
        {           
            _soundPlayer.Play("splat_wall", SoundPlayer.Bank.Multi);
            _meshRenderer.enabled = false;
            return;
        }
        
        _soundPlayer.Play("splat_enemy", SoundPlayer.Bank.Multi);
        health.TakeDamage(_damageAmount);
        _meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!_meshRenderer.enabled)
            return;

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0 )
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void Activate(Vector3 position, Vector3 velocity)
    {
        _meshRenderer.enabled = true;
        _lifeTimer = _maxLifetime;
        _rigidbody.position = position;
        _rigidbody.velocity = velocity;
        //_soundPlayer.Play("whoosh", SoundPlayer.Bank.Single);
    }
}

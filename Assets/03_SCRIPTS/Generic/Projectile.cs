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

    bool _isEnabled = false;

    public bool isActive { get { return _isEnabled; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _soundPlayer = GetComponent<SoundPlayer>();        
    }  

    private void OnTriggerEnter(Collider collision)
    {
        if (!_isEnabled)
            return;
        Debug.Log("Trigger enter with: " + collision.gameObject.name);
        Health health = collision.transform.GetComponent<Health>();
        if (health == null)
        {           
            _soundPlayer.Play("splat_wall", SoundPlayer.Bank.Multi);
            Disable();
            return;
        }
        
        _soundPlayer.Play("splat_enemy", SoundPlayer.Bank.Multi);
        health.TakeDamage(_damageAmount);
        Disable();
    }

    private void Update()
    {
        if (!_isEnabled)
            return;

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer < 0 )
        {
            Disable();
            return;
        }
    }

    public void Activate(Vector3 position, Vector3 velocity)
    {
        Enable();
        _lifeTimer = _maxLifetime;
        _rigidbody.position = position;
        _rigidbody.velocity = velocity;
        //_soundPlayer.Play("whoosh", SoundPlayer.Bank.Single);
    }

    private void Enable()
    {
        _isEnabled = true;
        _meshRenderer.enabled = true;
    }

    private void Disable()
    {
        _isEnabled = false;
        _meshRenderer.enabled = false;
        _rigidbody.velocity = Vector3.zero;
    }
}

using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] float _ammoToPickup;
    [SerializeField] string _playerTag;

    [Header("Bobbing")]
    [SerializeField] GameObject _sprite;
    [SerializeField] Transform _bobCeiling;
    [SerializeField] Transform _bobFloor;
    [SerializeField] float _bobSpeed;
    [SerializeField] float _delay;

    SoundPlayer _soundplayer;
    SpriteRenderer _spriteRenderer;
    float _delayTimer = 0.0f;
    Transform _target;
    Collider _collider;
    bool _isWaiting = false;   
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _sprite.transform.position = _bobCeiling.position;
        _target = _bobFloor;
        _soundplayer = GetComponent<SoundPlayer>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isWaiting)
        {            
            _delayTimer -= Time.deltaTime;
            if (_delayTimer <= 0)
            {
                _isWaiting = false;                
                _target = (_target == _bobCeiling) ? _bobFloor : _bobCeiling;
            }
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {        
        float step = _bobSpeed * Time.deltaTime;
        _sprite.transform.position = Vector3.MoveTowards(_sprite.transform.position, _target.transform.position, step);

        if (_sprite.transform.position == _target.position)
        {
            _isWaiting = true;
            _delayTimer = _delay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag(_playerTag))
        {            
            if (other.TryGetComponent<Ammo>(out var _playerAmmo))
            {
                _soundplayer.Play("pickup", SoundPlayer.Bank.Single);
                _playerAmmo.AmmoPickup(_ammoToPickup);               
                _spriteRenderer.enabled = false;
                _collider.enabled = false;
            }
            else
            {
                Debug.LogError("A Player tagged game object w/o an ammo script has hit the ammo pickup: " + gameObject.name);
            }
           
        }
    }
   
}

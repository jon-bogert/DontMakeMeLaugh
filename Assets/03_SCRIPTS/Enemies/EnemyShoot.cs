using UnityEngine;

[RequireComponent (typeof(ProjectilePool))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 10f;
    [SerializeField] Transform _firePoint;
    [Range(0, 1)]
    [SerializeField] float _leadChance = 0.5f;
    [SerializeField] ComedianAttackSounds _attackSoud;

    PlayerMovement _player;
    ProjectilePool _projectilePool;
    bool _setupSaid = false;
    float _setupLength;
    bool _firing = false;
    float _timer = 0;
    bool _firstShot = false;

    public bool setupSaid { get { return _setupSaid; } }

    private void Awake()
    {
        _projectilePool = GetComponent<ProjectilePool>();
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();       
        if (_player == null)
            Debug.LogError("EnemyShoot -> Could not find Player in scene");
    }

    public void Fire()
    {
        _setupLength = _attackSoud.setupLength;
        if (!_firstShot)
        {
            _firing = true;
            _firstShot = true;
            _attackSoud.PlaySetup();
        }

        else if (_setupSaid)
        {
            _firing = true;
        }
        
    }

    private void Update()
    {
       
        
        if (_firing && !_setupSaid && _firstShot)
        {
            
            _timer += Time.deltaTime;            
            Vector3 velocity = CalcVelocity();                   
                          
            if (_timer > _setupLength + 0.1f)
            {
                _projectilePool.FireNext(_firePoint.position, velocity);
                _setupSaid = true;
                _firing = false;
            }           
        }
        else if (_firing && _setupSaid)
        {
            Vector3 velocity = CalcVelocity();
            _projectilePool.FireNext(_firePoint.position, velocity);
            _firing = false;            
            
            
        }
        
        
    }

    private Vector3 CalcVelocity()
    {
        float chance = Random.Range(0f, 1f);
        if (chance < _leadChance)
        {
            Vector3 displacement = _player.transform.position - transform.position;
            float time = displacement.magnitude / _projectileSpeed;
            Vector3 velocity = displacement / time + _player.velocity;
            return velocity;
        }

        Vector3 v = _player.transform.position - transform.position;
        v.Normalize();
        v *= _projectileSpeed;
        return v;
    }
}

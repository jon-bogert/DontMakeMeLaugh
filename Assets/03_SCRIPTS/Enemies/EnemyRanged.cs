using UnityEngine;
using UnityEngine.Events;
using XephTools;

[RequireComponent(typeof(CharacterController))]
public class EnemyRanged : MonoBehaviour
{

    [SerializeField] float _patrolSpeed = 3f;
    [SerializeField] float _detectionRange = 5f;
    [SerializeField] float _idleTime = 3f;
    [SerializeField] float _patrolArriveDistance = 1f;
    [SerializeField] float _attackRate = 2f;
    [SerializeField] Transform[] _patrolPoints = new Transform[0];
    [SerializeField] LayerMask _wallcheckMask;

    [SerializeField] Texture _attackTexture;
    [SerializeField] Texture _deadTexture;

    [Header("References")]
    [SerializeField] Transform _camera;

    [Header("Events")]
    [SerializeField] UnityEvent onAttack;

    CharacterController _charController;
    ProjectilePool _projectilePool;
    StateMachine<EnemyRanged> _stateMachine;
    Material _material;
    int _moveTarget = 0;
    Vector3 _velocity = Vector3.zero;

    public float patrolSpeed { get { return _patrolSpeed; } }
    public float idleTime { get { return _idleTime; } }
    public float patrolArriveDistance { get {  return _patrolArriveDistance; } }
    public float attackRate { get {  return _attackRate; } }
    public Vector3 playerPosition {  get { return _camera.position; } }
    public bool isPlayerDetected
    {
        get
        {
            float p = (_camera.position - transform.position).sqrMagnitude;
            float w = float.MaxValue;
            RaycastHit[] hitInfo = Physics.RaycastAll(transform.position, (_camera.position - transform.position).normalized, _detectionRange, _wallcheckMask);
            if (hitInfo.Length == 0)
            {
                return p <= _detectionRange * _detectionRange;
            }
            foreach (RaycastHit hit in hitInfo)
            {
                if (hit.distance < w)
                    w = hit.distance;
            }
            w *= w;
            return p <= _detectionRange * _detectionRange && w > p;
        }
    }
    public Vector3 moveTarget {  get { return _patrolPoints[_moveTarget].position; } }
    public float deathLineLength { get { return 3f; } } //TODO get from audio length
    public EnemyState currentState { get { return (EnemyState)_stateMachine.currentState; } }
    public bool doesPatrol { get { return _patrolPoints != null &&  _patrolPoints.Length > 0;} }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _projectilePool = GetComponent<ProjectilePool>();

        _stateMachine = new StateMachine<EnemyRanged>(this);
        _stateMachine.AddState<RangedIdle>();
        _stateMachine.AddState<RangedPatrol>();
        _stateMachine.AddState<RangedAttack>();
        _stateMachine.AddState<RangedDead>();
        _stateMachine.AddState<RangedXtraDead>();
        _stateMachine.ChangeState((int)EnemyState.Idle);

    }
    private void Start()
    {
        if (_camera == null)
            _camera = Camera.main.transform;

        for (int i = 0; i < _patrolPoints.Length; ++i)
        {
            _patrolPoints[i].SetParent(null);
        }

        _material = GetComponentInChildren<MeshRenderer>().material;
        if (_material == null)
            Debug.LogError("Enemy Ranged \"" + name + "\" was unable find it's material");
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
        _charController.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }

    public void ChangeState(EnemyState state)
    {
        _stateMachine.ChangeState((int)state);
    }

    internal void NextPatrolPoint()
    {
        _moveTarget = (_moveTarget + 1) % _patrolPoints.Length;
    }

    internal void SetVelocity(Vector3 veclocity)
    {
        _velocity = veclocity;
    }
    internal void SetAttackTexture()
    {
        if (_attackTexture != null)
            _material.SetTexture("_MainTex", _attackTexture);
        else
            Debug.LogWarning("Enemy Melee \"" + name + "\" Attack texture was null");
    }

    internal void Attack()
    {
        Debug.Log(name + " is Attacking");
        onAttack?.Invoke();
    }

    internal void PlayDeathLine()
    {
        Debug.Log(name + ": Play death line");
    }

    internal void SanityBoost()
    {
        Debug.Log(name + ": Sanity Boost");
    }

    public void Kill()
    {
        if (currentState == EnemyState.Dead)
        {
            ChangeState(EnemyState.XtraDead);
            return;
        }

        if (_deadTexture != null)
        {
            _material.SetTexture("_MainTex", _deadTexture);
            _material.SetTexture("_LeftTex", _deadTexture);
            _material.SetTexture("_RightTex", _deadTexture);
            _material.SetTexture("_BackTex", _deadTexture);
        }
        else
        {
            Debug.LogWarning("Enemy Ranged \"" + name + "\" Dead texture was null");
        }

        ChangeState(EnemyState.Dead);
    }
}

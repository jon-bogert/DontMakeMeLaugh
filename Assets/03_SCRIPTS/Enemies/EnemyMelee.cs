using UnityEngine;
using UnityEngine.Events;
using XephTools;

public enum EnemyState { Idle, Patrol, Attack, Dead, XtraDead }

[RequireComponent(typeof(CharacterController))]
public class EnemyMelee : MonoBehaviour
{

    [SerializeField] float _patrolSpeed = 3f;
    [SerializeField] float _seekSpeed = 5f;
    [SerializeField] float _detectionRange = 5f;
    [SerializeField] float _idleTime = 3f;
    [SerializeField] float _patrolArriveDistance = 1f;
    [SerializeField] float _attackRate = 2f;
    [SerializeField] float _healAmount = 10f;
    [SerializeField] Transform[] _patrolPoints = new Transform[0];
    [SerializeField] LayerMask _wallcheckMask;

    [SerializeField] Texture _attackTexture;
    [SerializeField] Texture _deadTexture;

    [Header("References")]
    [SerializeField] Transform _camera;
    [SerializeField] SoundPlayer _dialougeSoundPlayer;

    [Header("Events")]
    [SerializeField] UnityEvent onAttack;
    
    CharacterController _charController;
    StateMachine<EnemyMelee> _stateMachine;
    Health _playerHealth;
    PlayerVoiceTrigger _voiceTrigger;
    Material _material;
    int _moveTarget = 0;
    Vector3 _velocity = Vector3.zero;
    
    public Vector3 velocityOverride = Vector3.zero;

    public float patrolSpeed { get { return _patrolSpeed; } }
    public float seekSpeed { get { return _seekSpeed; } }
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
            return p <= _detectionRange * _detectionRange &&  w > p;
        }
    }
    public Vector3 moveTarget {  get { return _patrolPoints[_moveTarget].position; } }
    public float deathLineLength { get { return 3f; } } //TODO get from audio length
    public EnemyState currentState { get { return (EnemyState)_stateMachine.currentState; } }
    public bool doesPatrol { get { return _patrolPoints != null &&  _patrolPoints.Length > 0;} }
    public LayerMask wallcheckMask { get { return _wallcheckMask; } }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();        

        _stateMachine = new StateMachine<EnemyMelee>(this);
        _stateMachine.AddState<MeleeIdle>();
        _stateMachine.AddState<MeleePatrol>();
        _stateMachine.AddState<MeleeAttack>();
        _stateMachine.AddState<MeleeDead>();
        _stateMachine.AddState<MeleeXtraDead>();
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
            Debug.LogError("Enemy Melee \"" + name + "\" was unable find it's material");

        _playerHealth = FindObjectOfType<PlayerMovement>().GetComponent<Health>();
        if (_playerHealth == null)
            Debug.LogWarning("EnemyMelee could not find Player's health component");

        _voiceTrigger = _playerHealth.GetComponent<PlayerVoiceTrigger>();
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
        if (velocityOverride != Vector3.zero)
        {
            velocityOverride.y = -10f;
            _charController.Move(velocityOverride * Time.deltaTime);
            velocityOverride = Vector3.zero;
        }
        else
        {
            _velocity.y = -10f;
            if (_charController.enabled)
                _charController.Move(_velocity * Time.deltaTime);
        }
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

    internal void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
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
        _dialougeSoundPlayer.Play("death", SoundPlayer.Bank.Single);
    }

    internal void OnXtraDead()
    {
        _charController.enabled = false;
        _playerHealth.Heal(_healAmount);
    }

    public void Kill()
    {
        if (currentState == EnemyState.XtraDead)
            return;
            
        _voiceTrigger.Invoke();

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
            Debug.LogWarning("Enemy Melee \"" + name + "\" Dead texture was null");
        }

        ChangeState(EnemyState.Dead);
    }
}

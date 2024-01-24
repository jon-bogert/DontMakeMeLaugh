using Unity.VisualScripting;
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
    [SerializeField] Transform[] _patrolPoints = new Transform[0];

    [Header("References")]
    [SerializeField] Transform _camera;

    [Header("Events")]
    [SerializeField] UnityEvent onAttack;

    CharacterController _charController;
    StateMachine<EnemyMelee> _stateMachine;
    int _moveTarget = 0;
    Vector3 _velocity = Vector3.zero;

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
            return (_camera.position - transform.position).sqrMagnitude <= _detectionRange * _detectionRange;
        }
    }
    public Vector3 moveTarget {  get { return _patrolPoints[_moveTarget].position; } }
    public float deathLineLength { get { return 3f; } } //TODO get from audio length
    public EnemyState currentState { get { return (EnemyState)_stateMachine.currentState; } }
    public bool doesPatrol { get { return _patrolPoints != null &&  _patrolPoints.Length > 0;} }

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
    }

    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
        DebugMonitor.UpdateValue("Enemy State", (EnemyState)_stateMachine.currentState);
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
        ChangeState(EnemyState.Dead);
    }
}

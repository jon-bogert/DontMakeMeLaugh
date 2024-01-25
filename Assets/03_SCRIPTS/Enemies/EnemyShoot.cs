using UnityEngine;

[RequireComponent (typeof(ProjectilePool))]
public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 10f;
    [SerializeField] Transform _firePoint;

    PlayerMovement _player;
    ProjectilePool _projectilePool;

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
        Vector3 velocity = CalcVelocity();;

        _projectilePool.FireNext(_firePoint.position, velocity);
    }

    private Vector3 CalcVelocity()
    {
        Vector3 displacement = _player.transform.position - transform.position;
        float time = displacement.magnitude / _projectileSpeed;
        Vector3 velocity = displacement / time + _player.velocity;
        return velocity;
    }
}

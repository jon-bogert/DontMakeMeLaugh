using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(ProjectilePool))]
[RequireComponent (typeof(Ammo))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float _fireRate = 1f;
    [SerializeField] float _projectileSpeed = 10f;

    [Header("References")]
    [SerializeField] Transform _firePoint;

    [Header("Inputs")]
    [SerializeField] InputActionReference _shootInput;

    ProjectilePool _projectilePool;
    SoundPlayer _soundPlayer;
    Ammo _ammo;
    float _fireTimer = 0;

    private void Awake()
    {
        _fireTimer = _fireRate; // so we can shoot right away
        _projectilePool = GetComponent<ProjectilePool>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _ammo = GetComponent<Ammo>();
        //get main camera if not assigned
        if (_firePoint == null)
            Debug.LogError("PlayerShoot -> Assign Fire Point in Inspector");

        _shootInput.action.performed += OnShootInput;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
    }

    private void OnDestroy()
    {
        _shootInput.action.performed -= OnShootInput;
    }

    private void OnShootInput(InputAction.CallbackContext ctx)
    {
        if (_fireTimer < _fireRate)
            return;
        if (_ammo.isClipEmpty)
            return;

        _ammo.Fire();
        _soundPlayer.Play("shoot", SoundPlayer.Bank.Multi);
        Vector3 velocity = _firePoint.forward * _projectileSpeed;
        _projectilePool.FireNext(_firePoint.transform.position, velocity);
    }
}

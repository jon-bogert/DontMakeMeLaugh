using UnityEngine;

/// <summary>
/// Goes on any object that shoots projectiles.
/// This object will instantiate it's own projectiles under a parent object it the Scene Level
/// </summary>

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] int _poolSize = 25;

    Projectile[] _projectiles;
    int _next = 0;
    private void Awake()
    {
        GameObject parentObject = Instantiate(new GameObject());
        parentObject.name = "PROJECTILE_POOL_" + name;
        _projectiles = new Projectile[_poolSize];
        for (int i = 0; i < _poolSize; ++i)
        {
            _projectiles[i] = Instantiate(_prefab, parentObject.transform).GetComponent<Projectile>();
        }
    }

    public void FireNext(Vector3 position, Vector3 velocity)
    {
        if (_projectiles[_next].isActive)
        {
            Debug.LogWarning("ProjectilePool " + name + ": Next projectile was already active. Consider increasing pool size");
        }
        _projectiles[_next].Activate(position, velocity);
        _next = (_next + 1) % _poolSize;
    }
}
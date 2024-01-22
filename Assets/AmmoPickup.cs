using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] float _ammoToPickup;
    [SerializeField] string _playerTag;

    SphereCollider _collider;    
    private void Start()
    {
        _collider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag(_playerTag))
        {
            if (other.TryGetComponent<Ammo>(out var _playerAmmo))
            {
                _playerAmmo.AmmoPickup(_ammoToPickup);
                if (_playerAmmo._clip == 0)
                {
                    _playerAmmo.Reload();
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("A Player tagged game object w/o an ammo script has hit the ammo pickup: " + gameObject.name);
            }
           
        }
        else
        {
            Debug.Log("Something not a player hit ammo pickup: " + gameObject.name);
        }
    }
   
}

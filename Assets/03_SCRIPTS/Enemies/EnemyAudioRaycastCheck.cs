using UnityEngine;
public class EnemyAudioRaycastCheck : MonoBehaviour 
{
    LayerMask _raycastMask;

    Transform _camera;

    void Awake()
    {
        _camera = Camera.main.transform;

        EnemyMelee melee = GetComponent<EnemyMelee>();
        if (melee != null)
        {
            _raycastMask = melee.wallcheckMask;
            return;
        }

        EnemyRanged ranged = GetComponent<EnemyRanged>();
        if (ranged != null)
        {
            _raycastMask = ranged.wallcheckMask;
            return;
        }

        Debug.LogError("EnemyAudioRaycastCheck '" + name + "' must be placed on an enemy");
    }

    public bool canPlaySound
    {
        get
        {
            float p = (_camera.position - transform.position).sqrMagnitude;
            float w = float.MaxValue;
            RaycastHit[] hitInfo = Physics.RaycastAll(transform.position, (_camera.position - transform.position).normalized, 100, _raycastMask);
            if (hitInfo.Length == 0)
            {
                return true;
            }
            foreach (RaycastHit hit in hitInfo)
            {
                if (hit.distance < w)
                    w = hit.distance;
            }
            w *= w;
            return w > p;
        }
    }
}

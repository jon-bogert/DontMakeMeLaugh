using UnityEngine;

public class LockBarrier : MonoBehaviour
{
    [SerializeField] GameObject _visual;

    private void Start()
    {
        if ( _visual.activeSelf == true )
        {
            _visual.SetActive(false);
        }
    }
}

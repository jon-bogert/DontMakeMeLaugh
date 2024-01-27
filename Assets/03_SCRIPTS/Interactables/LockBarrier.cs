using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class LockBarrier : MonoBehaviour
{
    [SerializeField] GameObject _visual;

    BoxCollider _boxCollider;

    private void Start()
    {
        if ( _visual.activeSelf == true )
        {
            _visual.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm == null)
            return;

        _visual.SetActive(true);
        _visual.transform.SetParent(null);

        gameObject.SetActive(false);
    }
}

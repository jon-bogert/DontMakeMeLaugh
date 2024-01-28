using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(BoxCollider))]
public class TriggerVolume : MonoBehaviour
{
    [SerializeField] UnityEvent _event;
    private void Start()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        if (!bc.isTrigger)
            bc.isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        BoxCollider bc = GetComponent<BoxCollider>();
        Gizmos.color = new Color (0f, 1f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, bc.size);   
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement pm = other.GetComponent<PlayerMovement>();
        if (pm == null)
            return;

        _event?.Invoke();
        Destroy(gameObject);
    }
}

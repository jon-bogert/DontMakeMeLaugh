using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class EnemyBarrier : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Color c = Color.red;
        c.a = 0.5f;
        Gizmos.color = c;

        Vector3 size = GetComponent<BoxCollider>().bounds.size;
        Gizmos.DrawCube(transform.position, size);
    }
}

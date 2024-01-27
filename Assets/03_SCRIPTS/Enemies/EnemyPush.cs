using UnityEngine;

[RequireComponent (typeof(CapsuleCollider))]
public class EnemyPush : MonoBehaviour
{
    [SerializeField] float _pushAmount = 1f;
    EnemyMelee _enemyMelee;

    private void Start()
    {
        _enemyMelee = GetComponentInParent<EnemyMelee>();
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 v = -(other.transform.position - transform.position);
        v.y = 0f;
        v.Normalize();
        v *= _pushAmount;

        _enemyMelee.velocityOverride = (v * Time.deltaTime);
    }
}

using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;
    public void Interact()
    {
        ParticleSystem ps = Instantiate(_particleEffectPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject);
    }
}

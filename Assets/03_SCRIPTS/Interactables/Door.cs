using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;
    [SerializeField] Health[] _requiredObjects = new Health[0];

    int _count = 0;

    private void Start()
    {
        _count = _requiredObjects.Length;
        foreach (Health h in _requiredObjects)
        {
            h.onDeath.AddListener(TrackDeath);
        }
    }

    public void Interact()
    {
        if (_count > 0)
            return;

        ParticleSystem ps = Instantiate(_particleEffectPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject);
    }

    void TrackDeath()
    {
        --_count;
    }
}

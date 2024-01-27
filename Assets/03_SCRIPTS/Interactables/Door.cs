using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;
    [SerializeField] Health[] _requiredObjects = new Health[0];

    KillEnemiesMsg _msg;

    int _count = 0;

    private void Start()
    {
        _count = _requiredObjects.Length;
        foreach (Health h in _requiredObjects)
        {
            h.onDeath.AddListener(TrackDeath);
        }
        _msg = FindObjectOfType<KillEnemiesMsg>();
    }

    public void Interact()
    {
        if (_count > 0)
        {
            _msg.Invoke();
            return;
        }

        ParticleSystem ps = Instantiate(_particleEffectPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(gameObject);
    }

    void TrackDeath()
    {
        --_count;
    }
}

using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _particleEffectPrefab;
    [SerializeField] Health[] _requiredObjects = new Health[0];

    KillEnemiesMsg _msg;
    SoundPlayer _soundPlayer;
    Collider _collider;
    MeshRenderer[] _meshRenderers;
    float _kickSoundLength;

    int _count = 0;

    private void Start()
    {
        _count = _requiredObjects.Length;
        _soundPlayer = GetComponent<SoundPlayer>();        
        _collider = GetComponent<Collider>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        Debug.Log(_meshRenderers.Length);
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
        _kickSoundLength = _soundPlayer.GetLengthOfSingle("kick");
        _soundPlayer.Play("kick", SoundPlayer.Bank.Single);
        _collider.enabled = false;
        foreach (MeshRenderer mesh in _meshRenderers)
        {
            mesh.enabled = false;
        }
        Destroy(gameObject, _kickSoundLength);
    }

    void TrackDeath()
    {
        --_count;
    }
}

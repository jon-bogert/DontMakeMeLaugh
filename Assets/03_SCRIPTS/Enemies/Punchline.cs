using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punchline : MonoBehaviour
{

    SoundPlayer _soundPlayer;
    float _punchLineLength;
    float _timer;
    Projectile _projectile;
    AudioSource _audioSource;
    bool _parentActive;

    private void Start()
    {
        _projectile = GetComponentInParent<Projectile>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _punchLineLength = _soundPlayer.GetLengthOfSingle("punchline");
        _timer = 0.01f;
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        _parentActive = _projectile.isActive;
        if (_parentActive)
        {
            _audioSource.enabled = true;
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {                
                _soundPlayer.Play("punchline", SoundPlayer.Bank.Single);
                _timer = _punchLineLength;
            }
        }
        else if (!_audioSource.isPlaying)
        {
            _audioSource.enabled = false;
        }       
        
    }    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedianAttackSounds : MonoBehaviour
{
    SoundPlayer _soundplayer;
    float _setupLength;
    EnemyAudioRaycastCheck _audioRay;
    public float setupLength { get { return _setupLength; } }

    private void Start()
    {
        _soundplayer = GetComponent<SoundPlayer>();
        _setupLength = _soundplayer.GetLengthOfSingle("setup");
        _audioRay = GetComponentInParent<EnemyAudioRaycastCheck>();
    }

    public void PlaySetup()
    {
        if (_audioRay.canPlaySound)
        {
            _soundplayer.Play("setup", SoundPlayer.Bank.Single);
        }        
    }
   
}

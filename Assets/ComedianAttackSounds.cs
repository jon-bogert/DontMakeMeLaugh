using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComedianAttackSounds : MonoBehaviour
{
    SoundPlayer _soundplayer;
    float _setupLength;
    public float setupLength { get { return _setupLength; } }

    private void Start()
    {
        _soundplayer = GetComponent<SoundPlayer>();
        _setupLength = _soundplayer.GetLengthOfSingle("setup");
    }

    public void PlaySetup()
    {
        _soundplayer.Play("setup", SoundPlayer.Bank.Single);
    }
   
}

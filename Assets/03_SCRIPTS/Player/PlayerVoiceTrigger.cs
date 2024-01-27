using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoiceTrigger : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float _voiceChance;
    [SerializeField] SoundPlayer _soundPlayer;

    public void Invoke()
    {
        float randomNum = Random.Range(0f, 1f);
        Debug.Log(randomNum);
        if (randomNum < _voiceChance)
        {            
            _soundPlayer.Play("reaction", SoundPlayer.Bank.Multi);
        }
        else
        {

        }
    }
}

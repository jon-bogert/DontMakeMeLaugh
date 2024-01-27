using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackSounds : MonoBehaviour
{
    
    SoundPlayer _soundPlayer;
    EnemyMoveSounds _enemyMoveSounds;

    private void Awake()
    {
        _soundPlayer = GetComponent<SoundPlayer>();
        _enemyMoveSounds = GetComponentInParent<EnemyMoveSounds>();
    }
  
    public void AttackSound()
    {        
        _soundPlayer.Play("attack", SoundPlayer.Bank.Multi);
    }
}

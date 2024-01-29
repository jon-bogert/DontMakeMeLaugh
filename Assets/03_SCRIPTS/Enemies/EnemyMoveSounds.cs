using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSounds : MonoBehaviour
{
    [SerializeField] float _stepTime = 0.2f;
    [SerializeField] public bool attacking = false;

    float _timer = 0f;
    SoundPlayer _soundPlayer;
    EnemyMelee _enemyMelee;
    EnemyRanged _enemyRanged;
    EnemyAudioRaycastCheck _audioRay;

    private void Awake()
    {
        _audioRay = GetComponentInParent<EnemyAudioRaycastCheck>();
        _soundPlayer = GetComponent<SoundPlayer>();
        if (gameObject.TryGetComponent<EnemyMelee>(out _enemyMelee))
        {
            
        }
        else if(gameObject.TryGetComponent<EnemyRanged>(out _enemyRanged))
        {
           
        }
        else
        {
            Debug.LogError("No Enemy Script Found");
        }

    }
    private void Update()
    {
        if (_enemyMelee != null)
        {
            if ((_enemyMelee.currentState == EnemyState.Patrol) || (_enemyMelee.currentState == EnemyState.Attack) && !attacking)
            {
                _timer += Time.deltaTime;
                if (_timer > _stepTime)
                {
                    _timer = 0f;
                    if (_audioRay.canPlaySound)
                    {
                        _soundPlayer.Play("step", SoundPlayer.Bank.Multi);
                    }                    
                }
            }
        }
        else if (_enemyRanged != null)
        {
            if ((_enemyRanged.currentState == EnemyState.Patrol) || (_enemyRanged.currentState == EnemyState.Attack))
            {
                _timer += Time.deltaTime;
                if (_timer > _stepTime)
                {
                    _timer = 0f;
                    if (_audioRay.canPlaySound)
                    {
                        _soundPlayer.Play("step", SoundPlayer.Bank.Multi);
                    }                    
                }
            }

        }
        
    }
}

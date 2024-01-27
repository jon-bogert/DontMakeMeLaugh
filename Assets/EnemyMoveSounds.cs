using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSounds : MonoBehaviour
{
    [SerializeField] float _stepTime = 0.2f;

    float timer = 0f;

    SoundPlayer _soundPlayer;
    EnemyMelee _enemyMelee;
    EnemyRanged _enemyRanged;

    private void Awake()
    {
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
            if ((_enemyMelee.currentState == EnemyState.Patrol) || (_enemyMelee.currentState == EnemyState.Attack) && (_enemyMelee.isPlayerDetected)
            {
                timer += Time.deltaTime;
                if (timer > _stepTime)
                {
                    timer = 0f;
                    _soundPlayer.Play("step", SoundPlayer.Bank.Multi);
                }
            }
        }
        else if (_enemyRanged != null)
        {
            if ((_enemyRanged.currentState == EnemyState.Patrol) || (_enemyRanged.currentState == EnemyState.Attack))
            {
                timer += Time.deltaTime;
                if (timer > _stepTime)
                {
                    timer = 0f;
                    _soundPlayer.Play("step", SoundPlayer.Bank.Multi);
                }
            }

        }
        
    }
}

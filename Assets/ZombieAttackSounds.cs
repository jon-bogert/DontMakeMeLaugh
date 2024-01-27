using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackSounds : MonoBehaviour
{
    float _timer = 0f;
    float _attackTime;

    SoundPlayer _soundPlayer;
    EnemyMelee _enemyMelee;
    Collider _hurtbox;

    private void Awake()
    {
        _soundPlayer = GetComponent<SoundPlayer>();
        _enemyMelee = gameObject.GetComponentInParent<EnemyMelee>();
        _attackTime = _enemyMelee.attackRate;
        _hurtbox = GetComponent<Collider>();
    }
    private void Update()
    {
        if (_enemyMelee != null)
        {
            if (_enemyMelee.currentState == EnemyState.Attack)
            {
                _timer += Time.deltaTime;
                if (_timer > _attackTime)
                {
                    _timer = 0f;
                    _soundPlayer.Play("attack", SoundPlayer.Bank.Multi);
                }
            }
        }       

    }
}

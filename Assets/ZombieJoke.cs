using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJoke : MonoBehaviour
{   

    EnemyMelee _zombie;
    Health _zombieHealth;
    SoundPlayer _soundPlayer;
    float _jokeLength;
    float _timer;

    private void Awake()
    {
        _zombie = GetComponentInParent<EnemyMelee>();
        _zombieHealth = _zombie.GetComponent<Health>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }
    private void Start()
    {

        _jokeLength = _soundPlayer.GetLengthOfSingle("joke");
        _timer = _jokeLength - 0.1f;
    }
    private void Update()
    {

        _timer += Time.deltaTime;
        if (_timer >= _jokeLength && _zombieHealth.health > 0)
        {
            _timer = 0f;
            _soundPlayer.Play("joke", SoundPlayer.Bank.Single);
        }
    }
}

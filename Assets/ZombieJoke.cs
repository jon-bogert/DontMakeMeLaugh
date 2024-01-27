using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJoke : MonoBehaviour
{
    [SerializeField] AudioClip _joke; 

    EnemyMelee _zombie;
    SoundPlayer _soundPlayer;
    float _jokeLength;
    float _timer;

    private void Awake()
    {
        _zombie = GetComponentInParent<EnemyMelee>();
        _soundPlayer = GetComponent<SoundPlayer>();
        _jokeLength = _joke.length;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _jokeLength)
        {
            _timer = 0f;
            _soundPlayer.Play("joke", SoundPlayer.Bank.Single);
        }
    }
}

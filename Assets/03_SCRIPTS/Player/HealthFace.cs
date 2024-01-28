using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HealthFace : MonoBehaviour
{
    
    [SerializeField] Sprite[] _faceSprites;
    [SerializeField] float _swapTime;

    Health _playerHealthScript;
    Image _face;
    float _swapTimer;
    float _playerHealth;
    int _arrayPair = 0;

    public void Start()
    {
        _playerHealthScript = GetComponentInParent<Health>();
        _face = GetComponent<Image>();
        _swapTimer = _swapTime;
    }
    private void Update()
    {
        CheckHealth();
        _swapTimer -= Time.deltaTime;       
        if (_swapTimer <= 0)
        {           
            Swap();
        }
    }

    public void CheckHealth()
    {
        _playerHealth = _playerHealthScript.health;
        
        if (_playerHealth <= 0)
        {
            _arrayPair = 9;
            Swap();
        }
        else if (_playerHealth <= 30)
        {
            _arrayPair = 7;
            Swap();
        }
        else if (_playerHealth <= 50)
        {
            _arrayPair = 5;
            Swap();
        }
        else if (_playerHealth <= 70)
        {
            _arrayPair = 3;
            Swap();
        }
        else
        {
            _arrayPair = 1;
            Swap();
        }
    }


    private void Swap()
    {        
        _swapTimer = _swapTime;
        //Debug.Log(_arrayPair);
        if (_arrayPair % 2 == 0)
        {
            _face.sprite = _faceSprites[_arrayPair + 1];
            _arrayPair += 1;
        }
        else
        {
            _face.sprite = _faceSprites[_arrayPair - 1];
            _arrayPair -= 1;
        }

    }


}

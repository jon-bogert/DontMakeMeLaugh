using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string _gameScene;
    [SerializeField] GameObject _creditsPanel;
    [SerializeField] AudioClip _titleTheme;

    AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _titleTheme;
        _audioSource.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void Credits()
    {
        if (!_creditsPanel.activeSelf)
        {
            _creditsPanel.SetActive(true);
        }
        else
        {
            _creditsPanel.SetActive(false);
        }
    }
    
    public void Quite()
    {
        Application.Quit();
    }


}

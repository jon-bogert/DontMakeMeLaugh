using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string _gameScene;
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _creditsPanel;

    AudioSource _audioSource;
    private void Start()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void Credits()
    {
        _creditsPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Settings()
    {
        _settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void Quite()
    {
        Application.Quit();
    }


}

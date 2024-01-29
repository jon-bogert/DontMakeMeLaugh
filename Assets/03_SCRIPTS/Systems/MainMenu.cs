using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string _gameScene;
    [SerializeField] GameObject _settingsPanel;

    private void Start()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameScene);
    }
    public void Settings()
    {
        _settingsPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quite()
    {
        Application.Quit();
    }


}

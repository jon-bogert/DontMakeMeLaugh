using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    [SerializeField] GameObject[] _screenObjects = new GameObject[0];
    [SerializeField] GameObject[] _otherObjects = new GameObject[0];
    [Tooltip("Should be the same as the up time on the damage FX")]
    [SerializeField] float _delay = 0f;

    float _timer = 0f;
    bool _showScreen = false;

    public void Invoke()
    {
        Time.timeScale = 0f;
        _showScreen = true;
    }

    private void Update()
    {
        if (!_showScreen)
            return;

        if (_timer < _delay)
        {
            _timer += Time.unscaledDeltaTime;
            return;
        }

        for (int i = 0; i < _screenObjects.Length; ++i)
        {
            _screenObjects[i].SetActive(true);
        }

        for (int i = 0; i < _otherObjects.Length; ++i)
        {
            _otherObjects[i].SetActive(false);
        }
    }

    public void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void GotoMainMenu()
    {
        //Time.timeScale = 1f;
        Debug.Log("Load Main Menu Here");
    }
}

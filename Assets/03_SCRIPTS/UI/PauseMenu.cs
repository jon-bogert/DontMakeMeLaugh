using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference _pauseInput;
    [SerializeField] GameObject _gameplayHUD;

    private void Awake()
    {
        _pauseInput.action.performed += OnPauseInput;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _pauseInput.action.performed -= OnPauseInput;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        _gameplayHUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        _gameplayHUD.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            Pause();
    }

    private void OnPauseInput(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0f)
            ResumeGame();
        else
            Pause();
    }
}

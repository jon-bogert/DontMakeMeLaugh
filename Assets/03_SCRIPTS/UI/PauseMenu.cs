using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    public void GotoMainMenu()
    {
        //Time.timeScale = 1f;
        Debug.Log("Goto Main Menu Here");
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        _gameplayHUD.SetActive(false);
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

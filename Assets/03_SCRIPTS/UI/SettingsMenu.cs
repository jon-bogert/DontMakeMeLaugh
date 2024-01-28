using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] InputActionReference _backInput;
    [SerializeField] GameObject _mainMenuObject;
    [SerializeField] TMP_Text _retroFXText;

    private void Update()
    {
        if (_backInput.action.WasPressedThisFrame())
        {
            FindObjectOfType<RetroEffects>().PollSettings();
            _mainMenuObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ToggleRetroFX()
    {
        AppSettings.useRetroEffects = !AppSettings.useRetroEffects;
        _retroFXText.text = (AppSettings.useRetroEffects) ? "ON" : "OFF";
    }
}

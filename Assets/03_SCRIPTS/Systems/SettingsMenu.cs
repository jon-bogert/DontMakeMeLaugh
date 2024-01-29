using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] InputActionReference _backInput;
    [SerializeField] GameObject _mainMenuObject;
    [SerializeField] Slider _mouseSlider;
    [SerializeField] Slider _gamepadSlider;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (_backInput.action.WasPressedThisFrame())
        {
            Back();
        }
    }

    public void AjustMouseUp()
    {
        AppSettings.mouseSensitivity = Mathf.Clamp(AppSettings.mouseSensitivity + 0.1f, 0.5f, 2f);
        UpdateUI();
    }

    public void AdjustMouseDown()
    {
        AppSettings.mouseSensitivity = Mathf.Clamp(AppSettings.mouseSensitivity - 0.1f, 0.5f, 2f);
        UpdateUI();
    }

    public void AdjustGamepadUp()
    {
        AppSettings.gamepadSensitivity = Mathf.Clamp(AppSettings.gamepadSensitivity + 0.1f, 0.5f, 2f);
        UpdateUI();
    }

    public void AdjustGamepadDown()
    {
        AppSettings.gamepadSensitivity = Mathf.Clamp(AppSettings.gamepadSensitivity - 0.1f, 0.5f, 2f);
        UpdateUI();
    }

    public void SliderChanged()
    {
        AppSettings.mouseSensitivity = _mouseSlider.value;
        AppSettings.gamepadSensitivity = _gamepadSlider.value;
    }

    void UpdateUI()
    {
        _mouseSlider.value = AppSettings.mouseSensitivity;
        _gamepadSlider.value = AppSettings.gamepadSensitivity;
    }

    public void Back()
    {
        _mainMenuObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

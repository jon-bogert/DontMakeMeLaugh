using UnityEngine;

public class RetroEffects : MonoBehaviour
{
    bool _effectOn = true;
    private void Start()
    {
        Screen.SetResolution(360, 240, true, 30);
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
    }

    public void Toggle()
    {
        _effectOn = !_effectOn;
        if (_effectOn)
        {
            Screen.SetResolution(360, 240, true, 30);
            Application.targetFrameRate = 30;
            QualitySettings.vSyncCount = 0;
            return;
        }
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
    }
}

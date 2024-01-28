using UnityEngine;

public class RetroEffects : MonoBehaviour
{

    private void Start()
    {
        Screen.SetResolution(360, 240, true, 30);
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;
    }
}

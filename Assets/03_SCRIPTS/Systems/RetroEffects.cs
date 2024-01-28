using UnityEngine;

public class RetroEffects : MonoBehaviour
{
    Bitcrusher[] _bitcrushers = new Bitcrusher[0];

    private void Start()
    {
        _bitcrushers = FindObjectsOfType<Bitcrusher>();
        PollSettings();
    }

    public void Activate()
    {
        Screen.SetResolution(360, 240, true, 30);
        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;

        for (int i = 0; i < _bitcrushers.Length; ++i)
        {
            _bitcrushers[i].enabled = true;
        }
    }

    public void Deactivate()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;

        for (int i = 0; i < _bitcrushers.Length; ++i)
        {
            _bitcrushers[i].enabled = false;
        }
    }

    public void PollSettings()
    {
        if (AppSettings.useRetroEffects)
            Activate();
        else
            Deactivate();
    }
}

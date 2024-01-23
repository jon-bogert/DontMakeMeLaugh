using UnityEngine;

public class RetroEffects : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(360, 240, true, 30);
        Application.targetFrameRate = 30;
    }
}

using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class GODMODE : MonoBehaviour
{
    static GODMODE instance;
    bool _godMode = false;
    public static bool isGodModeEnabled { get { return instance._godMode; } }

    private void Update()
    {
        if (!Debug.isDebugBuild)
            return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            _godMode = !_godMode;
            Debug.Log("<color=magenta>GODMODE: " + _godMode.ToString() + "</color>");
        }
    }

    private void Awake()
    {
        if (!Debug.isDebugBuild)
        {
            _godMode = false;
        }

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}

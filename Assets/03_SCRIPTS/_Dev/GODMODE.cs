using Unity.VisualScripting;
using UnityEngine;

public class GODMODE : MonoBehaviour
{
    static GODMODE instance;
    [SerializeField] bool _godMode = false;
    public static bool isGodModeEnabled { get { return instance._godMode; } }

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

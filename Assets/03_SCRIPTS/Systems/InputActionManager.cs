using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionManager : MonoBehaviour
{
    [SerializeField] InputActionAsset _inputs;

    private void Awake()
    {
        _inputs.Enable();
    }

    private void OnDestroy()
    {
        _inputs.Disable();
    }
}

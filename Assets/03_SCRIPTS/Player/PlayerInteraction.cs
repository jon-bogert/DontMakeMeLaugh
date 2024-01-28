using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    Door _interactable = null;
    [SerializeField] InputActionReference _interactInput;

    private void Awake()
    {
        _interactInput.action.performed += OnInteractInput;
    }

    private void OnDestroy()
    {
        _interactInput.action.performed -= OnInteractInput;
    }

    private void OnTriggerEnter(Collider other)
    {
        Door i = other.GetComponent<Door>();
        if (i == null)
            return;

        _interactable = i;
    }

    private void OnTriggerExit(Collider other)
    {
        Door i = other.GetComponent<Door>();
        if (i == _interactable)
            _interactable = null;
    }

    void OnInteractInput(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled)
            return;
        if (_interactable == null)
            return;

        _interactable.Interact();
    }
}

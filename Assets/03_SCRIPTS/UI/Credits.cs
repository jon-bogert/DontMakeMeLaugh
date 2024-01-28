using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 0.1f;
    [SerializeField] float _speedScrollSpeed = 1f;
    [SerializeField] InputActionReference _speedScrollInput;

    private void Update()
    {
        transform.Translate(Vector2.up *
            ((_speedScrollInput.action.ReadValue<float>() < 0.5f) ? 
            _scrollSpeed :
            _speedScrollSpeed)
            * Time.deltaTime);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("DeleteFile");
    }
}

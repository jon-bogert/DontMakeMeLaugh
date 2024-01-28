using UnityEngine;
using XephTools;

public class Credits : MonoBehaviour
{
    [SerializeField] float _scrollSpeed = 0.1f;
    [SerializeField] float _time = 30f;

    bool _hasStarted = false;
    float _timer = 0f;

    private void Update()
    {
        if (!_hasStarted)
            return;
        transform.Translate(Vector2.up * _scrollSpeed * Time.deltaTime);
        DebugMonitor.UpdateValue("Credits Timer", _timer);
        _timer += Time.deltaTime;
        if (_timer >= _time)
            GotoMainMenu();
    }

    public void Invoke()
    {
        _hasStarted = true;
    }

    public void GotoMainMenu()
    {
        Debug.Log("Goto Main Menu Here");
    }
}

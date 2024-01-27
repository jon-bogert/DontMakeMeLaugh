using TMPro;
using UnityEngine;

[RequireComponent (typeof(TMP_Text))]
public class KillEnemiesMsg : MonoBehaviour
{
    private enum State { Off, Up, Down }
    [SerializeField] float _upTime = 3f;

    TMP_Text _text;
    Color _color = Color.white;

    State _state = State.Off;
    float _timer;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _color = _text.color;
        _color = Transp(_color, 0f);
    }

    private void Update()
    {
        if (_state == State.Off)
            return;
        float t = 0f;
        if (_state == State.Up)
        {
            if (_timer > _upTime)
            {
                _state = State.Down;
                _timer = 0f;
                _color = Transp(_color, 1f);
                return;
            }
            t = Mathf.Lerp(0f, 1f, _timer / _upTime);
        }
        if (_state == State.Up)
        {
            if (_timer > _upTime)
            {
                _state = State.Off;
                _timer = 0f;
                _color = Transp(_color, 0f);
                return;
            }
            t = Mathf.Lerp(1f, 0f, _timer / _upTime);
        }
        Transp(_color, t);
        _timer += Time.deltaTime;
    }

    private static Color Transp(Color c, float val)
    {
        return new Color(c.r, c.g, c.b, 0f);
    }

    public void Invoke()
    {
        if (_state != State.Off)
            return;

        _state = State.Up;
    }
}

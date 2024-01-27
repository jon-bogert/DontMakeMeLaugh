using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    private enum State { Off, Up, Down }
    [SerializeField] float _timeUp = 0.05f;
    [SerializeField] float _timeDown = 0.05f;
    [SerializeField] float _maxOpacity = 0.75f;
    float _timer = 0;
    State _state = State.Off;

    Material _material;

    private void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        _material = mr.material;
    }

    private void Update()
    {
        if (_state == State.Off)
            return;

        float val = 0f;
        if (_state == State.Up)
        {
            if (_timer >= _timeUp)
            {
                _timer = 0;
                _state = State.Down;
                SetColor(1f);
                return;
            }
            val = Mathf.Lerp(0f, 1f, _timer / _timeUp);
            SetColor(val);
            _timer += Time.unscaledDeltaTime;
        }
        else // State Down
        {
            if (_timer >= _timeDown)
            {
                _timer = 0;
                _state = State.Off;
                SetColor(0f);
                return;
            }
            val = Mathf.Lerp(1f, 0f, _timer / _timeDown);
            SetColor(val);
            _timer += Time.deltaTime;
        }

    }

    private void SetColor(float val)
    {
        Color c = _material.GetColor("_BaseColor");
        c = new Color(c.r, c.g, c.b, val * _maxOpacity);
        _material.SetColor("_BaseColor", c);
    }

    public void Invoke()
    {
        _timer = 0f;
        _state = State.Up;
    }
}

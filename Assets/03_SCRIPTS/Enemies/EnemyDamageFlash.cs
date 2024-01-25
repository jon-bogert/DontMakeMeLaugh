using UnityEngine;

public class EnemyDamageFlash : MonoBehaviour
{
    private enum State { Off, Up, Down }
    [SerializeField] float _timeUp = 0.05f;
    [SerializeField] float _timeDown = 0.05f;
    float _timer = 0;
    State _state = State.Off;

    Material _material;

    private void Start()
    {
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
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
                _material.SetFloat("_DamageValue", 1f);
                return;
            }
            val = Mathf.Lerp(0f, 1f, _timer / _timeUp);
        }
        else // State Down
        {
            if (_timer >= _timeDown)
            {
                _timer = 0;
                _state = State.Off;
                _material.SetFloat("_DamageValue", 0f);
                return;
            }
            val = Mathf.Lerp(1f, 0f, _timer / _timeDown);
        }

        _material.SetFloat("_DamageValue", val);
        _timer += Time.deltaTime;
    }

    public void Invoke()
    {
        _timer = 0f;
        _state = State.Up;
    }
}

using UnityEngine;

public class Curtain : MonoBehaviour
{
    enum State { Off, Wait, Rise }

    [SerializeField] float _delay = 5f;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _timeToRise = 10f;

    State _state = State.Off;
    float _timer = 0f;

    private void Update()
    {
        if (_state == State.Off)
            return;
        if (_state == State.Wait)
        {
            if (_timer >= _delay)
            {
                _timer = 0f;
                _state = State.Rise;
                return;
            }
            _timer += Time.deltaTime;
            return;
        }

        if (_timer > _timeToRise)
        {
            _timer = 0f;
            _state = State.Off;
            return;
        }

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + _speed * Time.deltaTime,
            transform.position.z);

        _timer += Time.deltaTime;
    }


    public void Invoke()
    {
        _state = State.Wait;
    }
}

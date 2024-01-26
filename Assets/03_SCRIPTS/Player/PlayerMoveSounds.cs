using UnityEngine;

public class PlayerMoveSounds : MonoBehaviour
{
    [SerializeField] float _stepTime = 0.5f;
    [SerializeField] float _sprintStepTime = 0.3f;

    float timer = 0f;

    SoundPlayer _soundPlayer;
    PlayerMovement _playerMovement;

    private void Awake()
    {
        _soundPlayer = GetComponent<SoundPlayer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!_playerMovement.isMoving)
            return;

        float time = (_playerMovement.isSprinting) ? _sprintStepTime : _stepTime;
        timer += Time.deltaTime;
        if (timer > time)
        {
            timer = 0f;
            _soundPlayer.Play("step", SoundPlayer.Bank.Multi);
        }
    }


}

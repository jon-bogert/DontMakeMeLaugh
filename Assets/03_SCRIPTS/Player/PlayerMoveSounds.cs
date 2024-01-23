using UnityEngine;

public class PlayerMoveSounds : MonoBehaviour
{
    [SerializeField] float _stepTime = 0.5f;
    [SerializeField] float _sprintStepTime = 0.3f;

    float timer = 0f;

    SoundManager _soundManager;
    PlayerMovement _playerMovement;

    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>(); // TODO - Change to singleton
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
            _soundManager.Play("step", SoundManager.Bank.Multi);
        }
    }


}

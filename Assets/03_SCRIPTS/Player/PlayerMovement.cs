using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public UnityEvent onLand;

    [Header("Moving")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _sprintSpeed = 8f;
    [SerializeField] bool _allowSprint = true;
    [Range(0f, 1f)]
    [SerializeField] float _velocitySmoothing = 0.9f;
    [SerializeField] float _gravity = -10f;
    [SerializeField] float _bobRate = 10f;
    [SerializeField] float _bobRateSprinting = 15f;
    [SerializeField] float _bobHeight = 0.1f;
    [SerializeField] float _bobReturnSpeed = 0.25f;
    [SerializeField] float _gunBobAmount = 0.5f;

    [Header("Looking")]
    [SerializeField] float _lookSpeed = 1f;

    [Header("Programming")]
    [SerializeField] float _slopeDetectionDistance = 0.1f;

    [Header("References")]
    [SerializeField] Transform _camera;
    [SerializeField] GameObject _gun;

    [Header("Inputs")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _sprintInput;
    [SerializeField] InputActionReference _lookInput_GAMEPAD;
    [SerializeField] InputActionReference _lookInput_MOUSE;
    [Range(0.5f, 3f)]
    [SerializeField] float _mouseSensitivity = 1f;
    [Range(0.5f, 3f)]
    [SerializeField] float _gamepadSensitivity = 1f;

    Vector3 _posLastFrame = Vector3.zero;
    Vector3 _velocity = Vector3.zero;
    bool _isMoving = false;
    float _cameraHeight = 1.5f;
    float _t = 0f;
    float _returnFrom = 1.5f;
    Vector3 _returnFromGun = Vector3.zero;
    float _returnTimer = 0;
    bool _isGrounded = false;
    Vector3 _gunPos = Vector3.zero;

    CharacterController _charController;

    public Vector3 velocity { get { return _velocity; } }
    public bool isMoving { get { return _isMoving;} }
    public bool isSprinting { get { return _sprintInput.action.IsPressed(); } }

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        // Get Camera if Not Assigned
        if (_camera == null)
            _camera = Camera.main.transform;

        _cameraHeight = _camera.localPosition.y;
        _posLastFrame = transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (_gun != null)
            _gunPos = _gun.transform.position;
    }

    private void Update()
    {
        GroundCheck();
        ApplyGravity();

        //Looking
        float lookAxis_GP = _lookInput_GAMEPAD.action.ReadValue<float>();
        float lookAxis_MS = _lookInput_MOUSE.action.ReadValue<float>();
        if (lookAxis_MS != 0)
        {
            LookMouse(lookAxis_MS);
        }
        else if (lookAxis_GP != 0)
        {
            LookGamepad(lookAxis_GP);
        }

        //Moveing
        Vector2 moveAxis = _moveInput.action.ReadValue<Vector2>();
        if (moveAxis.sqrMagnitude > 0f)
        {
            Move(moveAxis);
            //Just Started Moving
            if (!_isMoving)
            {
                _t = 0f;
            }
            _isMoving = true;
        }
        else
        {
            _velocity.x = _velocity.z = 0f;
            
            //Just stopped Moving
            if (_isMoving)
            {
                _returnTimer = _bobReturnSpeed;
                _returnFrom = _camera.localPosition.y;
                _returnFromGun = _gun.transform.position;
            }
            _isMoving = false;
        }

        ApplySlope();
        ApplyVelocity();

        Bobbing();
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void GroundCheck()
    {
        bool isGrounded = _charController.isGrounded;
        if (isGrounded && !_isGrounded) // Just landed
        {
            onLand?.Invoke();
        }
        _isGrounded = isGrounded;
    }

    void ApplyGravity()
    {
        if (_isGrounded)
        {
            _velocity.y = -0.2f;
            return;
        }

        _velocity.y += _gravity * Time.deltaTime;
    }

    private void LookMouse(float axis)
    {
        transform.Rotate(Vector3.up, axis * _lookSpeed * 0.001f * _mouseSensitivity * Time.timeScale);
    }

    private void LookGamepad(float axis)
    {
        transform.Rotate(Vector3.up, axis * _lookSpeed * Time.deltaTime * _gamepadSensitivity);
    }

    private void Move (Vector2 axis)
    {
        float speed = (_allowSprint && _sprintInput.action.IsPressed()) ? _sprintSpeed : _moveSpeed;
        Vector3 velocity = new Vector3(
            axis.x * speed,
            0,
            axis.y * speed);

        Vector3 fwd = new Vector3(_camera.forward.x, 0f, _camera.forward.z);
        fwd.Normalize();
        Vector3 rgt = new Vector3(_camera.right.x, 0f, _camera.right.z);
        rgt.Normalize();

        velocity = rgt * velocity.x + fwd * velocity.z;

        velocity.y += _velocity.y;

        //Apply
        _velocity = velocity;
    }

    private void ApplySlope()
    {
        Vector3 v = new Vector3(_velocity.x, 0f, _velocity.z);
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit info, _slopeDetectionDistance))
        {
            Quaternion slopeRot = Quaternion.FromToRotation(Vector3.up, info.normal);
            v = slopeRot * v;

            if (v.y >= 0f)
                return;

            v.y += _velocity.y;
            _velocity = v;
        }
    }

    private void ApplyVelocity()
    {
        //Don't Do all this math if we don't want to smooth the velocity;
        if (_velocitySmoothing == 0f)
        {
            _charController.Move(_velocity * Time.deltaTime);
            return;
        }

        Vector3 prevVelocity = new Vector3(
            transform.position.x -_posLastFrame.x,
            0f,
            transform.position.z - _posLastFrame.z);
        prevVelocity /= Time.deltaTime;

        _posLastFrame = transform.position;

        Vector3 lateralVelocity = new Vector3(
            _velocity.x,
            0f,
            _velocity.z);

        Vector3 finalVelocity = Vector3.Lerp(lateralVelocity, prevVelocity, _velocitySmoothing);
        finalVelocity.y = _velocity.y;

        _charController.Move(finalVelocity * Time.deltaTime);
    }

    private void Bobbing()
    {
        if (_isMoving)
        {
            float newY = _cameraHeight + _bobHeight * Mathf.Sin(_t);
            _camera.localPosition = new Vector3(
                _camera.localPosition.x,
                newY,
                _camera.localPosition.z);

            if (_gun != null)
            {
                float gunY = _gunPos.y + _gunBobAmount * 5f * Mathf.Sin(_t - 0.1f);
                float gunX = _gunPos.x + _gunBobAmount * 5f * Mathf.Sin(_t * 0.5f);
                _gun.transform.position = new Vector3(
                    gunX,
                    gunY,
                    _gun.transform.position.z);
            }

            float rate = (_allowSprint && _sprintInput.action.IsPressed()) ? _bobRateSprinting : _bobRate;
            _t += Time.deltaTime * rate;

            while (_t > 4 * Mathf.PI)
                _t -= 4 * Mathf.PI;
            return;
        }

        if (_returnTimer <= 0f)
            return;

        _returnTimer -= Time.deltaTime;
        float t = _returnTimer / _bobReturnSpeed;
        if (t <= 0f)
        {
            _camera.localPosition = new Vector3(
                _camera.localPosition.x,
                _cameraHeight,
                _camera.localPosition.z);

            _gun.transform.position = _gunPos;
            return;
        }

        float y = Mathf.Lerp(_cameraHeight, _returnFrom, t);
        _camera.localPosition = new Vector3(
                _camera.localPosition.x,
                y,
                _camera.localPosition.z);

        Vector3 pos = Vector3.Lerp(_gunPos, _returnFromGun, t);
        _gun.transform.position = pos;

    }
}

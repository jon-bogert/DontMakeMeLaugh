using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ammo : MonoBehaviour
{
    [SerializeField] float _ammo;
    [SerializeField] float _clip;  

    [SerializeField] float _clipMax;    
    [SerializeField] float _ammoMax;

    [Header ("References")]
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] Slider _clipSlider;

    [Header("Inputs")]
    [SerializeField] InputActionReference _reloadInput;

    public float total { get { return  _ammo; } }
    public float clip { get { return  _clip; } }
    public bool isClipEmpty { get {  return _clip == 0; } }

    private void Awake()
    {
        _reloadInput.action.performed += OnReloadInput;
    }

    private void Update()
    {
        if (_clip == 0)
        {
            Reload();
        }
    }

    private void OnDestroy()
    {
        _reloadInput.action.performed -= OnReloadInput;
    }

    public void Fire()
    {
        _clip -= 1;
        UpdateUI();
    }

    public void Reload()
    {
        if (_ammo != 0)
        {
            if (_clip != 0)
            {
                _ammo += _clip;
                _clip = 0;
            }
            _ammo -= _clipMax;
            _ammo = Mathf.Clamp(_ammo, 0, _ammoMax);
            _clip += _clipMax;
            _clip = Mathf.Clamp(_clip, 0, _clipMax);
            UpdateUI();
        }
        else
        {
            Debug.Log("Out of Ammo!");
        }
        
    }

    //Move to ammo pickup script?
    public void AmmoPickup(float pickupAmount)
    {
        if (_ammo < _ammoMax)
        {
            _ammo += pickupAmount;
            _ammo = Mathf.Clamp(_ammo, 0, _ammoMax);
            UpdateUI();
        }
        else
        {
            Debug.Log("Ammo Maxed");
        }
       
    }

    public void UpdateUI()
    {
        if (_ammoText == null || _clipSlider == null)
        {
            Debug.LogWarning("Ammo -> One or more UI Elements was null");
            return;
        }
        _ammoText.text = _ammo.ToString();
        _clipSlider.value = _clip;
    }
    
    private void OnReloadInput(InputAction.CallbackContext ctx)
    {
        Reload();
    }
}

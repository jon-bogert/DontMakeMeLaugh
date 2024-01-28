using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Ammo : MonoBehaviour
{
    [SerializeField] float _ammo;
    [SerializeField] float _clip;

    [SerializeField] float _reloadDelay;

    [SerializeField] float _clipMax;    
    [SerializeField] float _ammoMax;

    [Header ("References")]
    [SerializeField] TextMeshProUGUI _ammoText;
    [SerializeField] Transform _gunUpPOS;
    [SerializeField] Transform _gunDownPOS;
    [SerializeField] SoundPlayer _gunSoundPlayer;
    [SerializeField] Slider _clipSlider;
    [SerializeField] Image _leaf;

    [Header("Inputs")]
    [SerializeField] InputActionReference _reloadInput;

    bool _tryReload;
    float _timer;
    Color _ammoTextColor;
    SoundPlayer _soundPlayer;

    public float total { get { return  _ammo; } }
    public float clip { get { return  _clip; } }
    public bool isClipEmpty { get {  return _clip == 0; } }

    private void Awake()
    {
        _reloadInput.action.performed += OnReloadInput;        
    }

    private void Start()
    {
        UpdateUI();
        _ammoTextColor = _ammoText.color;
    }

    private void Update()
    {
        if (_clip <= 0)
        {            
            TryReload();            
        }
        if (_tryReload)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = _reloadDelay;
                _tryReload = false;
                Reload();
            }
        }
       
    }

    private void OnDestroy()
    {
        _reloadInput.action.performed -= OnReloadInput;
    }

    public void Fire()
    {
        if (GODMODE.isGodModeEnabled)
            return;       

        _clip -= 1;
        if (_clip <= 0)
        {
            _gunSoundPlayer.Play("empty", SoundPlayer.Bank.Single);
        }
        UpdateUI();
    }

    public void Reload()
    {
        _gunSoundPlayer.Play("empty", SoundPlayer.Bank.Single);
        _gunSoundPlayer.Play("reload", SoundPlayer.Bank.Single);
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
            _ammoText.color = Color.green;
            StartCoroutine(ChangeColorAfterTime(1));
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
        if (_clip == _clipMax)
        {
            _leaf.gameObject.SetActive(true);
        }
        else if (_clip < _clipMax)
        {
            _leaf.gameObject.SetActive(false);
        }
    }
    private void TryReload()
    {        
        _tryReload = true;
    }
    
    private void OnReloadInput(InputAction.CallbackContext ctx)
    {
        TryReload();
    }
    IEnumerator ChangeColorAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        _ammoText.color = _ammoTextColor;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Events")]
    [SerializeField] UnityEvent onActivate;
    [SerializeField] UnityEvent onRight;
    [SerializeField] UnityEvent onLeft;

    [Header("Elements")]
    [SerializeField] Image background;
    [SerializeField] Image arrowLeft;
    [SerializeField] Image arrowRight;
    [SerializeField] TMP_Text text;

    [SerializeField] string activateAudioTag;
    [SerializeField] string hoverAudioTag;

    [SerializeField] Color selectColor;
    [SerializeField] Color deselectColor;

    AudioSource audioSource;

    bool isSelected = false;

    bool useBackground = true;
    bool useArrowLeft = true;
    bool useArrowRight = true;

    bool isFirstUpadate = true;
    bool canClick = false;
    float clickTimer = 0f;
    float clickBuffer = 0.1f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Initialize() // To be called by MenuManager (not GameLoader)
    {
        //background.color = deselectColor;
        //TODO - Start order issue -> This needs to go
        arrowLeft.gameObject.SetActive(false);
        arrowRight.gameObject.SetActive(false);
    }
    void OnEnable()
    {
        isFirstUpadate = true;
    }
    void Update()
    {
        if (isFirstUpadate)
            isFirstUpadate = false;

        if (!canClick && clickTimer < clickBuffer)
        {
            clickTimer += Time.unscaledDeltaTime;
        }
        else if (!canClick)
        {
            canClick = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelected = true;
        OnSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isSelected = false;
        OnDeselect();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFirstUpadate) return;
        if (!canClick) return;
        Activate();
    }

    public void SetUsingElements(bool useBG, bool useLeft, bool useRight)
    {
        useBackground = useBG;
        useArrowLeft = useLeft;
        useArrowRight = useRight;

        background.gameObject.SetActive(useBackground);
    }

    public void SetIsSelected(bool setTo)
    {
        isSelected = setTo;
        if (isSelected)
            OnSelect();
        else
            OnDeselect();
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void Activate()
    {
        if (isFirstUpadate) return;
        if (!isActiveAndEnabled) return;
        if (onActivate is null) return;
        onActivate.Invoke();
        PlaySound();
    }

    void OnSelect()
    {
        if (useBackground)
            background.color = selectColor;
        if (useArrowLeft)
            arrowLeft.gameObject.SetActive(true);
        if (useArrowRight)
            arrowRight.gameObject.SetActive(true);

        text.fontStyle = FontStyles.Bold;
    }

    void OnDeselect()
    {
        if (useBackground)
            background.color = deselectColor;
        arrowLeft.gameObject.SetActive(false);
        arrowRight.gameObject.SetActive(false);

        text.fontStyle = FontStyles.Normal;
    }

    void PlaySound()
    {
    }

    // ===== INPUT EVENTS =====

    public void InputRight()
    {
        if (onRight is null) return;
        onRight.Invoke();
        PlaySound();
    }

   public void InputLeft()
    {
        if (onLeft is null) return;
        onLeft.Invoke();
        PlaySound();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    int numButtons;
    bool[] selectionBuffer;
    int currIndex = 0;

    [Header("Functionality")]
    [SerializeField] bool useWrapAround = false;
    
    [Header("Buttons")]
    [SerializeField] CustomButton[] buttonsInOrder;
    [SerializeField] bool useBackground = true;
    [SerializeField] bool useArrowLeft = true;
    [SerializeField] bool useArrowRight = true;

    [Header("Input")]
    [SerializeField] InputActionReference inputConfirm;
    [SerializeField] InputActionReference inputUp;
    [SerializeField] InputActionReference inputDown;
    [SerializeField] InputActionReference inputRight;
    [SerializeField] InputActionReference inputLeft;

    void Awake()
    {
        inputConfirm.action.performed += InputConfirm;
        inputUp.action.performed += InputUp;
        inputDown.action.performed += InputDown;
        inputRight.action.performed += InputRight;
        inputLeft.action.performed += InputLeft;
    }
    void Start()
    {
        numButtons = buttonsInOrder.Length;
        if (numButtons == 0) Debug.LogWarning("MenuManager: No buttons assigned in inspector");

        for (int i = 0; i < numButtons; ++i)
            buttonsInOrder[i].Initialize();

        for (int i = 0; i < numButtons; ++i)
            buttonsInOrder[i].SetUsingElements(useBackground, useArrowLeft, useArrowRight);
        selectionBuffer = new bool[numButtons];
        selectionBuffer[0] = true;

        buttonsInOrder[0].SetIsSelected(true);

        for (int i = 1; i < numButtons; ++i)
        {
            selectionBuffer[i] = false;
            buttonsInOrder[i].SetIsSelected(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numButtons; ++i)
        {
            if (selectionBuffer[i] && !buttonsInOrder[i].GetIsSelected()) selectionBuffer[i] = false;
            if (!selectionBuffer[i] && buttonsInOrder[i].GetIsSelected())
            {
                ResetAllBut(i);
                currIndex = i;
                break;
            }
        }
    }

    void OnDestroy()
    {
        inputConfirm.action.performed -= InputConfirm;
        inputUp.action.performed -= InputUp;
        inputDown.action.performed -= InputDown;
        inputRight.action.performed -= InputRight;
        inputLeft.action.performed -= InputLeft;
    }

    //Member Functions
    void ResetAllBut(int index)
    {
        for (int i = 0; i < numButtons; i++)
        {
            if (i != index)
            {
                buttonsInOrder[i].SetIsSelected(false);
                selectionBuffer[i] = false;
            }
            else
            {
                selectionBuffer[i] = true;
            }
        }
    }

    void UpdateButtons(int oldIndex)
    {
        if (currIndex != oldIndex)
        {
            buttonsInOrder[oldIndex].SetIsSelected(false);
            buttonsInOrder[currIndex].SetIsSelected(true);
            selectionBuffer[oldIndex] = false;
            selectionBuffer[currIndex] = true;
        }
    }

    //Input Actions

    void InputConfirm(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled) return;

        buttonsInOrder[currIndex].Activate();
    }

    void InputUp(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled) return;

        int oldIndex = currIndex;
        if (currIndex == 0)
        {
            currIndex = (useWrapAround) ? numButtons - 1 : 0;
        }
        else currIndex--;

        UpdateButtons(oldIndex);
    }
    void InputDown(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled) return;

        int oldIndex = currIndex;
        if (currIndex == numButtons - 1)
        {
            currIndex = (useWrapAround) ? 0 : numButtons - 1;
        }
        else currIndex++;
        
        UpdateButtons(oldIndex);
    }

    void InputLeft(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled) return;
        buttonsInOrder[currIndex].InputLeft();
    }

    void InputRight(InputAction.CallbackContext ctx)
    {
        if (!isActiveAndEnabled) return;
        buttonsInOrder[currIndex].InputRight();
    }
}

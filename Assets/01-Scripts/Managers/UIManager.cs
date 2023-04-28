using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject clickBlocker;
    [SerializeField] private PopupDisplay errorDisplay;
    [SerializeField] private PopupDisplay descriptionDisplay;
    
    private Stack<PopupDisplay> _popupStack;

    private void Awake()
    {
        _popupStack = new Stack<PopupDisplay>();
    }

    public void DisplayErrorMessage(string message)
    {
        if(_popupStack.Count == 0)
            clickBlocker.SetActive(true);
        
        errorDisplay.ShowMessage(message);
        _popupStack.Push(errorDisplay);
    }

    public void DisplayDescription(BlockData data)
    {
        Debug.Log("UIManager.DisplayDescription");
        if(_popupStack.Count == 0)
            clickBlocker.SetActive(true);
        
        var separator = "" + GameManager.Instance.gameSettings.descriptionSeparator;
        var message = $"{data.grade}{separator}{data.domain}{separator}{data.cluster}{separator}{data.standardid}{separator}{data.standarddescription}";
        descriptionDisplay.ShowMessage(message);
        _popupStack.Push(descriptionDisplay);
        
    }

    public void ClosePopup()
    {
        _popupStack.Pop().Close();
        if(_popupStack.Count == 0)
            clickBlocker.SetActive(false);
    }

    public void ClosePopup(PopupDisplay display)
    {
        var tempStack = new Stack<PopupDisplay>();
        while (_popupStack.Peek() != display)
        {
            tempStack.Push(_popupStack.Pop());
        }

        _popupStack.Pop();
        
        if(tempStack.Count == 0)
            clickBlocker.SetActive(false);
        else
        {
            while (tempStack.Count > 0)
            {
                _popupStack.Push(tempStack.Pop());
            }
        }
    }
}
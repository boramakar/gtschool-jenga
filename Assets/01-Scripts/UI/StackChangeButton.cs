using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackChangeButton : MonoBehaviour
{
    [SerializeField] private StackChangeDirection changeDirection;
    
    private bool _canPress;

    private void Start()
    {
        _canPress = true;
    }

    private void OnEnable()
    {
        EventManager.OnToggleInputEvent += ToggleInput;
    }

    private void OnDisable()
    {
        EventManager.OnToggleInputEvent -= ToggleInput;
    }

    private void ToggleInput(bool isEnabled)
    {
        _canPress = isEnabled;
    }

    public void OnClick()
    {
        if (!_canPress) return;
        EventManager.ToggleInputEvent(false);
        ButtonPressEffect();
        EventManager.ChangeStackEvent(changeDirection);
    }

    private void ButtonPressEffect()
    {
        var gameSettings = GameManager.Instance.gameSettings;
        var punchDuration = gameSettings.buttonPressAnimationDuration;
        var punchScale = gameSettings.buttonPressAnimationScale;
            transform.DOPunchScale(Vector3.one * punchScale, punchDuration);
    }
}

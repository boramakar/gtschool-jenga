using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonAnimationHandler : MonoBehaviour
{
    [SerializeField] private ButtonAnimationType buttonAnimationType;
    
    public void OnClick()
    {
        switch (buttonAnimationType)
        {
            case ButtonAnimationType.Punch:
                PunchEffect();
                break;
            default:
                PunchEffect();
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PunchEffect()
    {
        var gameSettings = GameManager.Instance.gameSettings;
        var punchDuration = gameSettings.buttonPressAnimationDuration;
        var punchScale = gameSettings.buttonPressAnimationScale;
        transform.DOPunchScale(Vector3.one * punchScale, punchDuration);
    }
}

public enum ButtonAnimationType
{
    Punch
}

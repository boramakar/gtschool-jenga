using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraStackChange : MonoBehaviour
{
    private float _targetAngle;

    private void OnEnable()
    {
        EventManager.OnChangeStackEvent += ChangeStack;
    }

    private void OnDisable()
    {
        EventManager.OnChangeStackEvent -= ChangeStack;
    }

    private void ChangeStack(StackChangeDirection direction)
    {
        var angle = 360f / GameManager.Instance.Stacks.Count;
        _targetAngle = (direction) switch
        {
            StackChangeDirection.Next => _targetAngle - angle,
            StackChangeDirection.Prev => _targetAngle + angle
        };
        transform.DOLocalRotate(new Vector3(0, _targetAngle, 0), GameManager.Instance.gameSettings.StackChangeDuration)
            .OnComplete(() => EventManager.ToggleInputEvent(true));
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitalController : MonoBehaviour
{
    private Vector3 _lastMousePosition;
    private GameSettings _gameSettings;
    private float _xRotateSpeed;
    private float _yRotateSpeed;
    private Coroutine _coroutine;
    private float _zUpperLimit;
    private float _zLowerLimit;

    private void Awake()
    {
        _gameSettings = GameManager.Instance.gameSettings;
        _xRotateSpeed = _gameSettings.orbitalCameraXSpeed;
        _yRotateSpeed = _gameSettings.orbitalCameraYSpeed;
        _zUpperLimit = _gameSettings.orbitalCameraZUpperLimit;
        _zLowerLimit = _gameSettings.orbitalCameraZLowerLimit;
    }

    private void OnEnable()
    {
        EventManager.OnEnableOrbitalCamera += EnableOrbitalCamera;
        EventManager.OnDisableOrbitalCamera += DisableOrbitalCamera;
    }

    private void OnDisable()
    {
        EventManager.OnEnableOrbitalCamera -= EnableOrbitalCamera;
        EventManager.OnDisableOrbitalCamera -= DisableOrbitalCamera;
    }

    private void EnableOrbitalCamera()
    {
        _lastMousePosition = Input.mousePosition;
        _coroutine = StartCoroutine(MoveCamera());
    }

    private void DisableOrbitalCamera()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    IEnumerator MoveCamera()
    {
        while (true)
        {
            yield return null;
            var mousePosition = Input.mousePosition;
            var currentAngles = transform.localRotation.eulerAngles;
            var xRotation = (mousePosition.x - _lastMousePosition.x) * _xRotateSpeed;
            var zRotation = (mousePosition.y - _lastMousePosition.y) * _yRotateSpeed;
            if (currentAngles.z > 180)
                currentAngles.z -= 360;
            var clampedZAngle = Mathf.Clamp(currentAngles.z + zRotation, _zLowerLimit, _zUpperLimit);
            transform.localRotation = Quaternion.Euler(currentAngles.x, currentAngles.y + xRotation, clampedZAngle);
            _lastMousePosition = mousePosition;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    
    private GameManager _gameManager;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    private BlockData _data;
    private bool _isGhost;

    public void Initialize(BlockData data)
    {
        _gameManager = GameManager.Instance;
        _data = data;
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation.eulerAngles;
        _isGhost = true;
        if (TryGetComponent<Collider>(out _))
        {
            _isGhost = false;
        }
    }

    public void StartPhysicsSimulation()
    {
        if(_isGhost)
            gameObject.SetActive(false);
        else
            rigidbody.isKinematic = false;
    }

    public void EndPhysicsSimulation()
    {
        if(_isGhost)
            gameObject.SetActive(true);
        else
        {
            rigidbody.isKinematic = true;
            transform.DOLocalMove(_initialPosition, _gameManager.gameSettings.EndPhysicsMovementDuration);
            transform.DOLocalRotate(_initialRotation, _gameManager.gameSettings.EndPhysicsMovementDuration);
        }
    }
}

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

    public void Initialize(Transform parent, Vector3 position, Vector3 rotation)
    {
        _gameManager = GameManager.Instance;
        transform.parent = parent;
        transform.position = position;
        if (!TryGetComponent<Collider>(out _))
        {
            _isGhost = true;
        }
    }

    public void SetData(BlockData data)
    {
        _data = data;
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
            transform.DOMove(_initialPosition, _gameManager.gameSettings.EndPhysicsMovementDuration);
            transform.DORotate(_initialRotation, _gameManager.gameSettings.EndPhysicsMovementDuration);
        }
    }
}

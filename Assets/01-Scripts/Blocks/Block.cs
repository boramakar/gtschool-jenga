using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    
    public Vector3 initialPosition;
    public Vector3 initialRotation;
    
    private GameManager _gameManager;

    public void Initialize(Transform parent, Vector3 position, Vector3 rotation)
    {
        _gameManager = GameManager.Instance;
        transform.parent = parent;
        transform.position = position;
    }

    public void StartPhysicsSimulation()
    {
        rigidbody.isKinematic = false;
    }

    public void EndPhysicsSimulation()
    {
        rigidbody.isKinematic = true;
        transform.DOMove(initialPosition, _gameManager.gameSettings.EndPhysicsMovementDuration);
        transform.DORotate(initialRotation, _gameManager.gameSettings.EndPhysicsMovementDuration);
    }
}

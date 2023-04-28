using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BlockType blockType;
    
    private GameManager _gameManager;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    private BlockData _data;

    public void Initialize(BlockData data)
    {
        _gameManager = GameManager.Instance;
        _data = data;
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation.eulerAngles;
    }

    public void StartPhysicsSimulation()
    {
        if(blockType == BlockType.Glass)
            gameObject.SetActive(false);
        else
            rigidbody.isKinematic = false;
    }

    public void EndPhysicsSimulation()
    {
        if(blockType == BlockType.Glass)
            DOVirtual.DelayedCall(_gameManager.gameSettings.EndPhysicsMovementDuration, () => gameObject.SetActive(true));
        else
        {
            rigidbody.isKinematic = true;
            transform.DOLocalMove(_initialPosition, _gameManager.gameSettings.EndPhysicsMovementDuration);
            transform.DOLocalRotate(_initialRotation, _gameManager.gameSettings.EndPhysicsMovementDuration);
        }
    }

    public void DisplayDescription()
    {
        UIManager.Instance.DisplayDescription(_data);
    }
}

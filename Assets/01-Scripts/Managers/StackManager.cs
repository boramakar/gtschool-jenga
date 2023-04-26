using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    private GameManager _gameManager;
    private int _stackCount;
    private float _angleBetweenStacks;
    private List<StackHandler> stackList;
    private Stack<Vector3> positionList;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _stackCount = _gameManager.Stacks.Count;
        _angleBetweenStacks = 360f / _stackCount;
        FillPositionList();
    }

    private void Start()
    {
        GenerateStacks();
    }

    private void FillPositionList()
    {
        positionList = new Stack<Vector3>();
        
        var r = _gameManager.gameSettings.stackOffset;
        for (var i = _stackCount - 1; i >= 0; i--)
        {
            var radianAngle = Mathf.Deg2Rad * (_angleBetweenStacks * i);
            var xPos = r * Mathf.Cos(radianAngle);
            var zPos = r * Mathf.Sin(radianAngle);
            positionList.Push(new Vector3(xPos, 0, zPos));
        }
    }

    private void GenerateStacks()
    {
        stackList = new List<StackHandler>();
        
        var stackPrefab = _gameManager.gameSettings.stackPrefab;
        var stackOffset = _gameManager.gameSettings.stackOffset;
        
        foreach (var stackData in _gameManager.Stacks)
        {
            var stack = Instantiate(stackPrefab, positionList.Pop(), Quaternion.identity,
                transform).GetComponent<StackHandler>();
            stack.gameObject.name = $"Stack - {stackData.Key}";
            stackList.Add(stack);
            stack.transform.LookAt(transform);
            stack.Initialize(stackData.Key, stackData.Value);
        }
    }

    public void StartPhysicsSimulation()
    {
        foreach (var stack in stackList)
        {
            stack.StartPhysicsSimulation();
        }
    }

    public void EndPhysicsSimulation()
    {
        foreach (var stack in stackList)
        {
            stack.EndPhysicsSimulation();
        }
    }
}
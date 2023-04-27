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
    private int _activeStackIndex;
    private bool _isSimulationActive;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _stackCount = _gameManager.Stacks.Count;
        _angleBetweenStacks = 360f / _stackCount;
        _isSimulationActive = false;
        FillPositionList();
    }

    private void Start()
    {
        GenerateStacks();
    }

    private void OnEnable()
    {
        EventManager.OnChangeStackEvent += ChangeActiveStack;
        EventManager.OnStartPhysicsSimulation += StartPhysicsSimulation;
        EventManager.OnEndPhysicsSimulation += EndPhysicsSimulation;
    }

    private void OnDisable()
    {
        EventManager.OnChangeStackEvent -= ChangeActiveStack;
        EventManager.OnStartPhysicsSimulation += StartPhysicsSimulation;
        EventManager.OnEndPhysicsSimulation += EndPhysicsSimulation;
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

    private void ChangeActiveStack(StackChangeDirection direction)
    {
        if (_isSimulationActive)
        {
            EndPhysicsSimulation();
            _isSimulationActive = false;
        }
        
        _activeStackIndex = (direction) switch
        {
            StackChangeDirection.Next => (_activeStackIndex + 1) % stackList.Count,
            StackChangeDirection.Prev => (_activeStackIndex + (stackList.Count - 1)) % stackList.Count,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private void StartPhysicsSimulation()
    {
        stackList[_activeStackIndex].StartPhysicsSimulation();
        _isSimulationActive = true;
    }

    private void EndPhysicsSimulation()
    {
        stackList[_activeStackIndex].EndPhysicsSimulation();
        _isSimulationActive = false;
    }
}
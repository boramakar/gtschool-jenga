using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StackHandler : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameDisplay;

    private GameSettings _gameSettings;
    private List<RowHandler> rowList;

    public void Initialize(string stackName, List<BlockData> stackData)
    {
        _gameSettings = GameManager.Instance.gameSettings;
        rowList = new List<RowHandler>();

        var rowPrefab = _gameSettings.rowPrefab;
        var rowCount = Mathf.CeilToInt(stackData.Count / 3f);
        for (var i = 0; i < rowCount; i++)
        {
            var blockIndex = 3 * i;
            var rowOffset = Vector3.up * (i * _gameSettings.blockHeight);
            var row = Instantiate(rowPrefab, transform.position + rowOffset, transform.rotation, transform)
                .GetComponent<RowHandler>();
            row.GenerateBlocks(stackData[blockIndex],
                blockIndex + 1 < stackData.Count ? stackData[blockIndex + 1] : (BlockData?) null,
                blockIndex + 2 < stackData.Count ? stackData[blockIndex + 2] : (BlockData?) null);
            var rowRotation = i % 2 == 0 ? 0 : -90;
            row.transform.localRotation = Quaternion.Euler(0, rowRotation, 0);
            rowList.Add(row);
        }

        nameDisplay.text = stackName;
    }

    public void StartPhysicsSimulation()
    {
        foreach (var row in rowList)
        {
            row.StartPhysicsSimulation();
        }
    }

    public void EndPhysicsSimulation()
    {
        foreach (var row in rowList)
        {
            row.EndPhysicsSimulation();
        }
    }
}
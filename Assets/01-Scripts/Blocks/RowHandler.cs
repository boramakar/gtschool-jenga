using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowHandler : MonoBehaviour
{
    private Block _block1;
    private Block _block2;
    private Block _block3;

    public void GenerateBlocks(BlockData blockData1, BlockData? blockData2, BlockData? blockData3)
    {
        var blockOffset = GameManager.Instance.gameSettings.blockWidth;
        _block1 = Instantiate(GameManager.Instance.gameSettings.blockPrefabs[blockData1.mastery],
            transform.position + transform.right * -blockOffset, transform.rotation, transform).GetComponent<Block>();
        _block1.Initialize(blockData1);

        if (blockData2 != null)
        {
            _block2 = Instantiate(GameManager.Instance.gameSettings.blockPrefabs[blockData2.Value.mastery],
                transform.position, transform.rotation, transform).GetComponent<Block>();
            _block2.Initialize(blockData2.Value);
        }

        if (blockData3 != null)
        {
            _block3 = Instantiate(GameManager.Instance.gameSettings.blockPrefabs[blockData3.Value.mastery],
                transform.position + transform.right * blockOffset, transform.rotation, transform).GetComponent<Block>();
            _block3.Initialize(blockData3.Value);
        }
    }

    public void StartPhysicsSimulation()
    {
        _block1.StartPhysicsSimulation();
        if (_block2 != null)
            _block2.StartPhysicsSimulation();
        if (_block3 != null)
            _block3.StartPhysicsSimulation();
    }

    public void EndPhysicsSimulation()
    {
        _block1.EndPhysicsSimulation();
        if (_block2 != null)
            _block2.EndPhysicsSimulation();
        if (_block3 != null)
            _block3.EndPhysicsSimulation();
    }
}
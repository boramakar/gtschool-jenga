using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameSettings gameSettings;

    public Dictionary<string, List<BlockData>> Stacks;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.logger.logEnabled = false;
#endif

        SingletonCheck();
    }

    private void SingletonCheck()
    {
        if (Instance == this)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        LoadStackData();
    }

    private void ChangeScene(Scenes nextScene)
    {
        var sceneLoadOp = SceneManager.LoadSceneAsync((int) nextScene);
        sceneLoadOp.completed += SceneLoadComplete;
    }

    private void SceneLoadComplete(AsyncOperation op)
    {
        // Do stuff if necessary
    }

    private void LoadStackData()
    {
        NetworkManager.Instance.GetStack(OnLoadStackSuccess, OnLoadStackFail);
    }

    private void OnLoadStackSuccess(string response)
    {
        Debug.Log($"Success: {response}");
        var paddedResponse = $"{{\"blocks\":{response}}}";
        Debug.Log($"Padded: {paddedResponse}");
        var allBlocks = JsonUtility.FromJson<StackData>(paddedResponse);
        SplitStacks(allBlocks);
        ChangeScene(Scenes.GameScene);
    }

    private void OnLoadStackFail(string response)
    {
        Debug.Log($"Fail: {response}");
        UIManager.Instance.DisplayErrorMessage($"{Constants.checkNetworkConnectionError}\n{response}");
    }

    private void SplitStacks(StackData allBlocks)
    {
        Stacks = new Dictionary<string, List<BlockData>>();
        foreach (var blockData in allBlocks.blocks)
        {
            var stackName = blockData.grade;
            if (!Stacks.ContainsKey(stackName))
            {
                Stacks.Add(stackName, new List<BlockData>());
            }

            Stacks[stackName].Add(blockData);
        }

        SortStacks();
    }

    private void SortStacks()
    {
        var keyList = Stacks.Select(pair => pair.Key).ToList();

        foreach (var key in keyList)
        {
            var sortedList = new List<BlockData>(Stacks[key]);
            Stacks[key] = MergeSortBLockData(sortedList, 0, sortedList.Count - 1);
        }
    }

    private List<BlockData> MergeSortBLockData(List<BlockData> sortedList, int min, int max)
    {
        if (min < max)
        {
            int median = min + (max - min) / 2;
            MergeSortBLockData(sortedList, min, median);
            MergeSortBLockData(sortedList, median + 1, max);
            MergeBlockData(sortedList, min, median, max);
        }

        return sortedList;
    }

    private void MergeBlockData(List<BlockData> sortedList, int min, int median, int max)
    {
        int n1 = median - min + 1;
        int n2 = max - median;
        BlockData[] L = new BlockData[n1];
        BlockData[] R = new BlockData[n2];
        int i, j, k;
        for (i = 0; i < n1; i++)
        {
            L[i] = sortedList[min + i];
        }

        for (i = 0; i < n2; i++)
        {
            R[i] = sortedList[median + 1 + i];
        }

        i = 0;
        j = 0;
        k = min;
        while (i < n1 && j < n2)
        {
            var comparison = String.Compare(L[i].domain, R[j].domain, StringComparison.OrdinalIgnoreCase);
            if (comparison < 0)
            {
                sortedList[k++] = L[i++];
            }
            else if (comparison > 0)
            {
                sortedList[k++] = R[j++];
            }
            else
            {
                comparison = String.Compare(L[i].cluster, R[j].cluster, StringComparison.OrdinalIgnoreCase);
                if (comparison < 0)
                {
                    sortedList[k++] = L[i++];
                }
                else if (comparison > 0)
                {
                    sortedList[k++] = R[j++];
                }
                else
                {
                    comparison = String.Compare(L[i].standardid, R[j].standardid, StringComparison.OrdinalIgnoreCase);
                    if (comparison <= 0)
                    {
                        sortedList[k++] = L[i++];
                    }
                    else
                    {
                        sortedList[k++] = R[j++];
                    }
                }
            }
        }

        while (i < n1)
        {
            sortedList[k++] = L[i++];
        }

        while (j < n2)
        {
            sortedList[k++] = R[j++];
        }
    }
}
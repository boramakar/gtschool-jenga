using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameSettings gameSettings;

    public Dictionary<string, List<BlockData>> Stacks;
    // public ErrorMessages errorMessages;

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
        var sceneLoadOp = SceneManager.LoadSceneAsync((int)nextScene);
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
        var allBlocks =  JsonUtility.FromJson<StackData>(paddedResponse);
        SplitStacks(allBlocks);
        ChangeScene(Scenes.GameScene);
    }

    private void OnLoadStackFail(string response)
    {
        Debug.Log($"Fail: {response}");
        // UIManager.Instance.DisplayErrorMessage($"{errorMessages.checkNetworkConnection}\n{response}");
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
    }
}
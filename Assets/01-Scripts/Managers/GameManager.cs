using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameSettings gameSettings;
}

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    var obj = new GameObject(nameof(T));
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _instance;
        }
    }
}

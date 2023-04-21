using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : Singleton<NetworkManager>
{
    public delegate void NetworkCallback(string text);

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

    //Should include at least UserData for getting the correct stack in a real implementation
    public void GetStack(NetworkCallback successCallback, NetworkCallback failCallback)
    {
        StartCoroutine(_GetRequest(GameManager.Instance.gameSettings.sampleURI, successCallback, failCallback));
    }

    #region HTTP Requests

    IEnumerator _GetRequest(string uri, NetworkCallback SuccessCallback, NetworkCallback FailCallback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            var error = webRequest.error;
            var response = webRequest.downloadHandler.text;
            var webRequestResult = webRequest.result;
            webRequest.Dispose();

            switch (webRequestResult)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + error);
                    FailCallback(response);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + error);
                    Debug.Log("Error message: " + response);
                    FailCallback(response);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + response);
                    SuccessCallback?.Invoke(response);
                    break;
            }
        }
    }

    IEnumerator _PostRequest(string uri, WWWForm formData, NetworkCallback SuccessCallback,
        NetworkCallback FailCallback)
    {
        Debug.Log("Posting: " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, formData))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            var error = webRequest.error;
            var response = webRequest.downloadHandler.text;
            var webRequestResult = webRequest.result;
            webRequest.Dispose();

            switch (webRequestResult)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + error);
                    FailCallback?.Invoke(response);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + error);
                    Debug.Log("Error message: " + response);
                    FailCallback?.Invoke(response);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + response);
                    SuccessCallback?.Invoke(response);
                    break;
            }
        }
    }

    IEnumerator _PutRequest(string uri, string raw, NetworkCallback SuccessCallback, NetworkCallback FailCallback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(uri, raw))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            var error = webRequest.error;
            var response = webRequest.downloadHandler.text;
            var webRequestResult = webRequest.result;
            webRequest.Dispose();

            switch (webRequestResult)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + error);
                    FailCallback(response);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + error);
                    FailCallback(response);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + response);
                    SuccessCallback(response);
                    break;
            }
        }
    }

    #endregion
}
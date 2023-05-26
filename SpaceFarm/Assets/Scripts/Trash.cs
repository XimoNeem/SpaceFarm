using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Networking;

public class Trash : MonoBehaviour
{
    public StorageInfo data;

    private void Start()
    {
        GameEvents.Instance.OnResourcesChanged.AddListener(Save);
        string resourcesJson = JsonUtility.ToJson(data);
        Debug.Log(resourcesJson);
    }

    public void Done()
    {
        Debug.Log("Save");
    }

    public void Error(string message)
    {
        Debug.LogError(message);
    }

    public void Save()
    {
        Debug.Log("Saving...");
        StartCoroutine(DataBaseHandler.SaveUser(MainContext.Instance.User, Done, Error));
    }
}

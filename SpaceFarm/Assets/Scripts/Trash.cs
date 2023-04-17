using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;
using Game.Networking;

public class Trash : MonoBehaviour
{
    public void SaveBuildings()
    {
        string str = JsonUtility.ToJson(BuildMenager.Instance.Buildings);
        Debug.Log(str);
    }
    public void Save()
    {
        UserData data = new UserData(
                            1,
                            "Mikhail",
                            "testmail@gmail.com",
                            "qwerty12345"
                        );
        data.Resources = ResourceStorage.Instance.Storage;


        StartCoroutine(DataBaseHandler.SaveUser(
                data,
                delegate { Debug.Log("Done"); },
                PrintError
            ));
    }

    public void Load()
    {
        StartCoroutine(DataBaseHandler.LoadUserData(
                1,
                SetUser,
                PrintError
            ));
    }

    public void Add()
    {
        ResourceStorage.Instance.GetResourceItem(ResourceType.Gems).AddValue(1);
    }

    public void Lose()
    {
        ResourceStorage.Instance.GetResourceItem(ResourceType.Gems).LoseValue(1);
    }

    private void SetUser(UserData data)
    {
        ResourceStorage.Instance.Storage = data.Resources;
        GameObject.FindObjectOfType<EventBus>().OnResourcesChanged.Invoke();
    }

    private void PrintError(string text)
    {
        Debug.LogError(text);
    }
}

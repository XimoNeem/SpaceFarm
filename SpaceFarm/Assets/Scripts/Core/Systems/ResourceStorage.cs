using Game.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Data;

public class ResourceStorage : MonoBehaviour
{
    public static ResourceStorage Instance;

    public StorageInfo Storage;
    public ExchangeRate ExchangeRate;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    public void PrintError(string test)
    {
        Debug.LogError(test);
    }

    public void PrintUser(UserData data)
    {
        print(data.ToString());
        print(data.Resources.resources[0].Value);
    }

    public ResourceItem GetResourceItem(ResourceType type)
    {
        ResourceItem result = null;

        foreach (var resource in Storage.resources)
        {
            if (resource.Resource.Type == type)
            { 
                result = resource;
                return result;
            }
        }
        foreach (var resource in Storage.crops)
        {
            if (resource.Resource.Type == type)
            {
                result = resource;
                return result;
            }
        }

        return result;
    }

    public bool Trade(ResourceType fromType, ResourceType toType, int value)
    {

        if (GetResourceItem(fromType).LoseValue(value))
        {
            GetResourceItem(toType).AddValue(ExchangeRate.GetRate(fromType, toType) * value);
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class ResourceItem
{
    public int Value = 0;
    public Resource Resource;

    public bool AddValue(int value)
    {
        Value += value;

        GameObject.FindObjectOfType<EventBus>().OnResourcesChanged.Invoke();

        return true;
    }

    public bool LoseValue(int value)
    {
        if (Value - value >= 0)
        {
            Value -= value;

            GameObject.FindObjectOfType<EventBus>().OnResourcesChanged.Invoke();

            return true;
        }
        else { return false; }
    }
}

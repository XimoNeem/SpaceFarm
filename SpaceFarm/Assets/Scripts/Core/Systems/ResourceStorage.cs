using UnityEngine;
using Game.Data;

[System.Serializable]
public class ResourceStorage
{
    public StorageInfo Resources;

    public ResourceStorage(StorageInfo storage)
    {
        Resources = storage;
    }

    public ResourceStorage()
    {

    }

    public ResourceItem GetResourceItem(ResourceType type)
    {
        ResourceItem result = null;

        foreach (var resource in Resources.resources)
        {
            if (resource.Resource.Type == type)
            { 
                result = resource;
                return result;
            }
        }
        foreach (var resource in Resources.crops)
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
            GetResourceItem(toType).AddValue(MainContext.Instance.ExchangeRate.GetRate(fromType, toType) * value);
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

        GameObject.FindObjectOfType<GameEvents>().OnResourcesChanged.Invoke();

        return true;
    }

    public bool LoseValue(int value)
    {
        if (Value - value >= 0)
        {
            Value -= value;

            GameObject.FindObjectOfType<GameEvents>().OnResourcesChanged.Invoke();

            return true;
        }
        else { return false; }
    }
}

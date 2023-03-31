using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    public ResourceItem[] resources;
    public ResourceItem[] crops;
    public static ResourceStorage Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
    }

    public ResourceItem GetResourceItem(ResourceType type)
    {
        ResourceItem result = null;

        foreach (var resource in resources)
        {
            if (resource.Resource.Type == type) { result = resource; }
        }

        return result;
    }

    public bool Trade(ResourceType fromType, ResourceType toType, int value)
    {
        return true;
    }
}

[System.Serializable]
public class ResourceItem
{
    public int Value = 0;
    public Resource Resource;

    public void AddValue(int value)
    {
        Value += value;

        GameObject.FindObjectOfType<EventBus>().OnResourcesChanged.Invoke();
    }

    public void LoseValue(int value)
    {
        if (Value - value > 0)
        {
            Value -= value;

            GameObject.FindObjectOfType<EventBus>().OnResourcesChanged.Invoke();
        }
    }
}

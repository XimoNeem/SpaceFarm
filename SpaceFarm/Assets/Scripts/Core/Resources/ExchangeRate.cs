using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExchangeRate", menuName = "Traiding/ExchangeRate asset")]
[System.Serializable]
public class ExchangeRate : ScriptableObject
{
    public ResourceRate[] ResourceRates;

    public int GetRate(ResourceType fromType, ResourceType toType)
    {
        foreach (var item in ResourceRates)
        {
            if (item.Type == fromType)
            {
                return item.GetRate(toType);
            }
        }

        throw new Game.Exeptions.NoSuchResourceExeption();
    }
}

[System.Serializable]
public class ResourceRate
{
    public ResourceType Type;
    public ResourceItem[] Rates = new ResourceItem[4];

    public int GetRate(ResourceType toType)
    {
        foreach (var item in Rates)
        {
            if (item.Resource.Type == toType)
            {
                return item.Value;
            }
        }

        throw new Game.Exeptions.NoSuchResourceExeption();
    }
}
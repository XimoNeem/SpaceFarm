using UnityEngine;

[System.Serializable]
public class Resource : ScriptableObject
{
    public Sprite Icon;
    public ResourceType Type;
    public string Name;
    public string Description;
}

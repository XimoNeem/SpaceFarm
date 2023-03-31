using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ProductItem", menuName = "Resources/Create ProductItem")]
public class Product : Resource
{
    public Sprite[] Sprites;
    public ResourceItem[] PriceItems;
}

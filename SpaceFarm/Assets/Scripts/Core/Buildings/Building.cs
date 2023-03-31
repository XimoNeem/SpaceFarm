using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool IsCreated = false;
    public int Height = 1, Width = 1;
    public int Depth;
    public SpriteRenderer Renderer;
    private ResourceStorage _ResourceStorage;
    public Tile CurrentTile;

    private int level;
    public ResourceItem[] IncomeItems;

    public virtual void Start()
    {
        
        try
        {
            Renderer = GetComponentInChildren<SpriteRenderer>();
        }
        catch (System.Exception)
        {
            throw;
        }
        _ResourceStorage = GetComponent<ResourceStorage>();
    }

    /// <summary>
    /// Эта функция вызывется при создании построки
    /// </summary>
    /// <param name="tile"></param>
    public virtual void Create(Tile tile)
    {
        CurrentTile = tile;
        IsCreated = true;
        EventBus.Instance.OnTime.AddListener(GetIncome);
    }

    public void SetDepth(int value)
    {
        Depth = value;
        if (Renderer != null)
        {
            Renderer.sortingOrder = Depth;
        }
        this.transform.position += -Vector3.forward;
    }

    public virtual void OnClick()
    {
        
    }

    virtual public void GetIncome()
    {
        foreach (ResourceItem resource in IncomeItems)
        {
            ResourceItem currentItem = ResourceStorage.Instance.GetResourceItem(resource.Resource.Type);
            
            if (currentItem != null)
            {
                currentItem.AddValue(resource.Value);
            }
        }
    }

    virtual public void UpgradeBuilding()
    {

    }
}

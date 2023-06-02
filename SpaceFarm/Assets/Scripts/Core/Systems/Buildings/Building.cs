using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int ID;
    public bool IsCreated = false;
    public BuildingType Type;
    public int Height = 1, Width = 1;
    public int Depth;
    public SpriteRenderer Renderer;
    public Tile CurrentTile;

    public int Level;
    public int Progress;
    public ResourceItem[] IncomeItems;

    private void Awake()
    {
        try
        {
            Renderer = GetComponentInChildren<SpriteRenderer>();
        }
        catch (System.Exception)
        {
            throw;
        }

        SetDepth(Depth);
    }

    public virtual void Start()
    {

    }

    /// <summary>
    /// Эта функция вызывется при создании построки
    /// </summary>
    /// <param name="tile"></param>
    public virtual void Initialize(Tile tile)
    {
        CurrentTile = tile;
        CurrentTile.TileBuilding = this;
        IsCreated = true;
    }

    public void SetDepth(int value)
    {
        Depth = value;
        if (Renderer == null) { return; }
        
        Renderer.sortingOrder = Depth;
        this.transform.position += -Vector3.forward;
    }

    public void SetTile(Tile tile)
    {
        CurrentTile = tile;
    }

    public virtual void OnClick()
    {
        
    }

    virtual public void GetIncome()
    {

    }

    virtual public void UpgradeBuilding()
    {

    }
}

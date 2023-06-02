using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private BuildingInfo _currentBuilding;

    public BuildingInfo[] BuildingsInfo;

    public Dictionary<int, BuildingInfo> Buildings;

    private Building _buildingToCreate;
    private Tile _targetTile;

    public bool BuildMode { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Buildings = new Dictionary<int, BuildingInfo>();

        foreach (var item in BuildingsInfo)
        {
            Buildings.Add(item.buildingPrefab.ID, item);
        }
    }

    private void Start()
    {
        GameEvents.Instance.OnTileClicked.AddListener(SetTargetTile);
    }

    public void EnterBuildingMode()
    {
        ShowGrid(true);
        BuildMode = true;

        _buildingToCreate = Instantiate(_currentBuilding.buildingPrefab, new Vector3(-100, -100, 0), Quaternion.identity);
    }

    public void SetBuilding(BuildingInfo newBuilding)
    {
        _currentBuilding = newBuilding;
    }

    public void SetTargetTile(Tile target)
    {
        if (target.isOccupied()) { return; }
        if (!BuildMode) { return; }

        _targetTile = target;

        _buildingToCreate.transform.position = target.transform.position;
        _buildingToCreate.transform.localScale = target.transform.localScale;
        _buildingToCreate.SetTile(_targetTile);
        _buildingToCreate.SetDepth(target.depth + 1);

        if (target.Behaviour.TryBuild(_buildingToCreate.GetComponent<Building>()))
        {
            FindObjectOfType<Constructing_WindowControler>().SetApplyButtonActive(true);
            _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        else
        {
            FindObjectOfType<Constructing_WindowControler>().SetApplyButtonActive(false);
            _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    public void CreateBuilding()
    {
        _buildingToCreate.Initialize(_targetTile);
        _buildingToCreate.Renderer.color = Color.white;
        _targetTile.SetOccupied(true);

        BuildMode = false;

        foreach (var item in _currentBuilding.Price)
        {
            int costValue = item.Value;
            MainContext.Instance.User.Storage.GetResourceItem(item.Resource.Type).LoseValue(costValue);
        }

        GameEvents.Instance.OnBuildingCreated.Invoke();
        ShowGrid(false);
    }

    public void RestoreBuilding(Tile tile, Game.Data.BuildingSaveItem data, GameObject buildingPrefab)
    {
        Building building = Instantiate(buildingPrefab).GetComponent<Building>();

        building.SetDepth(tile.depth++);
        building.transform.position = tile.transform.position;
        building.Progress = data.Progress;
        building.Initialize(tile);
    }

    public void CancelBuilding()
    {
        Destroy(_buildingToCreate.gameObject);
        BuildMode = false;
        ShowGrid(false);
    }

    private void ShowGrid(bool state)
    {
        foreach (var item in FindObjectsOfType<Tile>())
        {
            if (state)
            {
                item.ChangeSize(0.95f);
            }
            else
            {
                item.ChangeSize(1);
            }
        }
    }
}

[System.Serializable]
public struct BuildingInfo
{
    public string Name;
    public Sprite Preview;
    public string Description;
    public ResourceItem[] Price;
    public Building buildingPrefab;
}


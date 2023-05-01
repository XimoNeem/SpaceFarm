using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private Building _currentBuilding;

    public BuildingsData Buildings;


    public BuildingInfo[] BuildingsInfo;

    private Building _buildingToCreate;
    private Tile _targetTile;

    public bool BuildMode { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Buildings = new BuildingsData();
    }

    private void Start()
    {
        GameEvents.Instance.OnTileClicked.AddListener(SetTargetTile);
    }

    public void EnterBuildingMode()
    {
        ShowGrid(true);
        BuildMode = true;

        _buildingToCreate = Instantiate(_currentBuilding, new Vector3(-100, -100, 0), Quaternion.identity);
    }

    public void SetBuilding(Building newBuilding)
    {
        _currentBuilding = newBuilding;
    }

    public void SetTargetTile(Tile target)
    {
        if (target.isOccupied()) { return; }
        if (!BuildMode) { return; }

        _targetTile = target;

        _buildingToCreate.transform.position = target.transform.position;
        _buildingToCreate.GetComponent<Building>().SetDepth(target.depth + 1);

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
        _buildingToCreate.GetComponent<Building>().Create(_targetTile);
        Buildings.Buildings.Add(_buildingToCreate.GetComponent<Building>());
        _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        BuildMode = false;
        _targetTile.SetOccupied(true);
        ShowGrid(false);
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

[System.Serializable]
public class BuildingsData
{
    public List<Building> Buildings;

    public BuildingsData()
    {
        Buildings = new List<Building>();
    }
}

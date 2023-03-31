using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenager : MonoBehaviour
{
    [SerializeField] private Building _currentBuilding;

    public static BuildMenager Instance;

    public BuildingInfo[] Buildings;

    private Building _buildingToCreate;
    private Tile _targetTile;

    public bool BuildMode { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EventBus.Instance.OnTileClicked.AddListener(SetTargetTile);
    }

    public void EnterBuildingMode()
    {
        ShowGrid(true);
        BuildMode = true;
        _buildingToCreate = Instantiate(_currentBuilding,
            position: new Vector3(-100, -100, 0), rotation: Quaternion.identity);
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

        if (target.TileBehaviour.TryBuild(_buildingToCreate.GetComponent<Building>()))
        {
            FindObjectOfType<Constructing_WindowControler>().SetApplyButtonActive(true);
            _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        else
        {
            FindObjectOfType<Constructing_WindowControler>().SetApplyButtonActive(true);
            _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }
    }

    public void CreateBuilding()
    {
        _buildingToCreate.GetComponent<Building>().Create(_targetTile);
        _buildingToCreate.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        BuildMode = false;
        _targetTile.SetOccupied(true);
        //ShowGrid(false);
    }

    public void CancelBuilding()
    {
        Destroy(_buildingToCreate);
        BuildMode = false;
        ShowGrid(false);
    }

    private void ShowGrid(bool state)
    {
        /*foreach (var item in FindObjectsOfType<Tile>())
        {
            if (state)
            {
                item.ChangeSize(0.9f);
            }
            else
            {
                item.ChangeSize(1);
            }

        }*/
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


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building_WindowControler : WindowController
{
    [Space(10)]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Image previewImage;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private Building_ViewItem itemPrefab;

    [SerializeField] private BuildingInfo _currenBuilding;

    private void CreateItemsList()
    {
        foreach (var item in MainContext.Instance.BuildSystem.BuildingsInfo)
        {
            CreateBuildingItem(item);
        }
    }

    private void CreateBuildingItem(BuildingInfo building_info)
    {
        Building_ViewItem helper = Instantiate(itemPrefab, scrollViewContent);
        helper._nameText.text = building_info.Name;
        helper._previeImage.sprite = building_info.Preview;
        helper._currentBuilding = building_info;
        helper._button.onClick.AddListener( delegate { SoundManager.Instance.PlaySound(SFXType.Click); } );
    }

    void Start()
    {
        CreateItemsList();
        SetCurrentBuilding(MainContext.Instance.BuildSystem.BuildingsInfo[0]);
    }

    public void SetCurrentBuilding(BuildingInfo buildingInfo)
    {
        MainContext.Instance.BuildSystem.SetBuilding(buildingInfo.buildingPrefab);
        _currenBuilding = buildingInfo;
        previewImage.sprite = _currenBuilding.Preview;
        nameText.text = _currenBuilding.Name;

        string desc = _currenBuilding.Description + "\n\n";

        foreach (ResourceItem item in _currenBuilding.Price)
        {
            desc += $"{item.Resource.Name} - {item.Value}\n";
        }

        descriptionText.text = desc;
    }

    public void Build()
    {
        bool state = true;

        foreach (var item in _currenBuilding.Price)
        {
            try
            {
                int costValue = item.Value;
                int playerValue = MainContext.Instance.Storage.GetResourceItem(item.Resource.Type).Value;

                if (costValue > playerValue)
                {
                    state = false;
                    break;
                }
            }
            catch (System.Exception){throw;}
            
        }

        if (state)
        {
            FindObjectOfType<BuildSystem>().EnterBuildingMode();
            FindObjectOfType<Main_WindowController>().ShowWindow(false);
            FindObjectOfType<Constructing_WindowControler>().ShowWindow(true);
            FindObjectOfType<Constructing_WindowControler>().SetApplyButtonActive(false);

            foreach (var item in _currenBuilding.Price)
            {
                int costValue = item.Value;
                MainContext.Instance.Storage.GetResourceItem(item.Resource.Type).LoseValue(costValue);
            }

            this.ShowWindow(false);
        }

        else { print("PLAYER ERROR: RESOV NEMA ;("); }
    }
}

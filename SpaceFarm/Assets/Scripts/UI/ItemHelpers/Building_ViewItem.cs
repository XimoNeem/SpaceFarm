using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class Building_ViewItem : MonoBehaviour
{
    public Image _previeImage;
    public TMP_Text _nameText;
    public Button _button;
    public BuildingInfo _currentBuilding;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        FindObjectOfType<Building_WindowControler>().SetCurrentBuilding(_currentBuilding);
    }
}

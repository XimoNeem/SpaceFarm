using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Traiding_ViewItem : MonoBehaviour
{
    public Image _previeImage;
    public TMP_Text _nameText;
    public TMP_Text _valueText;
    public Button _button;
    public Resource _currentResource;

    private void Start()
    {
        //GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field_WindowController : WindowController
{
    [SerializeField] private Product[] _crops;
    [SerializeField] private Transform _content;
    private Filed _currentField;

    private void Start()
    {
        CreateList();
    }

    private void CreateList()
    {
        foreach (var item in _crops)
        {
            GameObject newObject = new GameObject();
            newObject.transform.parent = _content;
            Image image = newObject.AddComponent<Image>();
            image.preserveAspect = true;
            image.sprite = item.Icon;
            Button button = newObject.AddComponent<Button>();
            button.onClick.AddListener( delegate { _currentField.SetCrop(item); this.ShowWindow(false); } );
        }
    }

    /// <summary>
    /// При нажитии на поле
    /// </summary>
    public void SetFileld(Filed field)
    {
        _currentField = field;
        ShowWindow(true);
    }
}
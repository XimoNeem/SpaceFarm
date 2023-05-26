using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Traiding_WindowController : WindowController
{
    [Space(10)]
    [SerializeField] private Transform _scrollViewContent;
    [SerializeField] private Traiding_ViewItem _itemPrefab;

    [SerializeField] private GameObject _sellWindow;
    [SerializeField] private TMP_Text _sellFromText, _sellToText;
    [SerializeField] private Image _sellFromImage, _sellToImage;

    [SerializeField] private Slider _sellSlider;

    [SerializeField] private ResourceType _fromType, _toType;
    private int _value = 0;

    private void OnEnable()
    {
        _sellSlider.onValueChanged.AddListener(GetSliderInput);
    }

    private void OnDisable()
    {
        _sellSlider.onValueChanged.RemoveListener(GetSliderInput);
    }

    public override void ShowWindow(bool state)
    {
        base.ShowWindow(state);

        if (state) { StartCoroutine(CreateItemsList()); }
        else { DestroyItems(); }
    }

    private IEnumerator CreateItemsList()
    {
        foreach (var item in MainContext.Instance.User.Storage.Resources.crops)
        {
            CreateTraidingItem(item);
            yield return new WaitForEndOfFrame();
        }
    }

    private void DestroyItems()
    {
        for (int i = 0; i < _scrollViewContent.childCount; i++)
        {
            Destroy(_scrollViewContent.GetChild(i).gameObject);
        }
    }

    private void CreateTraidingItem(ResourceItem resource)
    {
        Traiding_ViewItem helper = Instantiate(_itemPrefab, _scrollViewContent);
        helper._nameText.text = resource.Resource.Name;
        helper._valueText.text = resource.Value.ToString();
        helper._previeImage.sprite = resource.Resource.Icon;
        helper._currentResource = resource.Resource;
        helper._button.onClick.AddListener( delegate { MainContext.Instance.SoundManager.PlaySound(SFXType.Click); } );
        helper._button.onClick.AddListener( delegate { ShowSellWindow(resource, MainContext.Instance.User.Storage.GetResourceItem(ResourceType.Gold)); } );
    }

    private void ShowSellWindow(ResourceItem fromItem, ResourceItem toItem)
    {
        _sellWindow.SetActive(true);

        _sellFromText.text = "0";
        _sellFromImage.sprite = fromItem.Resource.Icon;

        _sellToText.text = "0";
        _sellToImage.sprite = toItem.Resource.Icon;

        _sellSlider.maxValue = fromItem.Value;
        _sellSlider.value = 0;

        _fromType = fromItem.Resource.Type;
        _toType = toItem.Resource.Type;
        _value = 0;
    }

    private void GetSliderInput(float value)
    {
        _value = (int)value;

        _sellFromText.text = value.ToString();
        _sellToText.text = (MainContext.Instance.ExchangeRate.GetRate(_fromType, _toType) * _value).ToString();
    }

    public void Trade()
    {
        if (MainContext.Instance.User.Storage.Trade(_fromType, _toType, _value))
        {
            _sellWindow.SetActive(false);
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}

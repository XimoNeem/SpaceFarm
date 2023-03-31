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
        foreach (var item in ResourceStorage.Instance.crops)
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
        helper._previeImage.sprite = resource.Resource.Icon;
        helper._currentResource = resource.Resource;
        helper._button.onClick.AddListener( delegate { SoundManager.Instance.PlaySound(SFXType.Click); } );
        helper._button.onClick.AddListener( delegate { ShowSellWindow(resource, ResourceStorage.Instance.GetResourceItem(ResourceType.Money)); } );
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
    }

    private void GetSliderInput(float value)
    {
        int val = (int)value;

        _sellFromText.text = value.ToString();

        _sellToText.text = value.ToString();
    }

    private void Trade()
    {
        
    }
}
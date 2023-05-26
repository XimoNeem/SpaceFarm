using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resource_ViewItem : MonoBehaviour
{
    [SerializeField] private TMP_Text textItem;
    [SerializeField] private Image icon;
    [SerializeField] private Currency _resource;

    private void Start()
    {
        textItem = GetComponentInChildren<TMP_Text>();

        icon.sprite = _resource.Icon;

        GameEvents.Instance.OnResourcesChanged.AddListener(RedrawUI);

        RedrawUI();
    }

    public void RedrawUI()
    {
        textItem.text = MainContext.Instance.User.Storage.GetResourceItem(_resource.Type).Value.ToString();
    }
}

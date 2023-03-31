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

        FindObjectOfType<EventBus>().OnResourcesChanged.AddListener(Redraw_ui);

        Redraw_ui();
    }

    public void Redraw_ui()
    {
        textItem.text = ResourceStorage.Instance.GetResourceItem(_resource.Type).Value.ToString();
    }
}

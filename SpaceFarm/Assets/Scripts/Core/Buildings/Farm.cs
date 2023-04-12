using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    [SerializeField] private int _progress = 0;
    [SerializeField] private int _maxProgress = 10;

    [SerializeField] private Sprite[] _sprites;

    public override void Create(Tile tile)
    {
        base.Create(tile);

        EventBus.Instance.OnMove.AddListener(GetIncome);
    }
    public override void GetIncome() // Каждые 10 секунд
    {
        _progress += 1;
        Debug.Log(_progress);

        if (_progress < _maxProgress)
        {
            Renderer.sprite = _sprites[_progress];
        }

        else if (_progress == _maxProgress)
        {
            _progress = 0;
            foreach (ResourceItem resource in IncomeItems)
            {
                ResourceStorage.Instance.GetResourceItem(resource.Resource.Type).AddValue(resource.Value);
            }
        }
    }
}

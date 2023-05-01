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

        GameEvents.Instance.OnMove.AddListener(GetIncome);
    }
    public override void GetIncome() // ������ 10 ������
    {
        _progress += 1;

        if (_progress < _maxProgress)
        {
            Renderer.sprite = _sprites[_progress];
        }

        else if (_progress == _maxProgress)
        {
            _progress = 0;
            foreach (ResourceItem resource in IncomeItems)
            {
                MainContext.Instance.Storage.GetResourceItem(resource.Resource.Type).AddValue(resource.Value);
            }
        }
    }
}
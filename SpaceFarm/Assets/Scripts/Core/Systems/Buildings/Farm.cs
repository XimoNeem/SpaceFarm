using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    [SerializeField] private int _maxProgress => _sprites.Length;

    [SerializeField] private Sprite[] _sprites;

    public override void Initialize(Tile tile)
    {
        base.Initialize(tile);

        GameEvents.Instance.OnMove.AddListener(GetIncome);
    }
    public override void GetIncome() // Каждые 10 секунд
    {
        Progress += 1;

        if (Progress < _maxProgress)
        {
            Renderer.sprite = _sprites[Progress];
        }

        else if (Progress == _maxProgress)
        {
            Progress = 0;
            foreach (ResourceItem resource in IncomeItems)
            {
                MainContext.Instance.User.Storage.GetResourceItem(resource.Resource.Type).AddValue(resource.Value);
            }
        }
    }
}

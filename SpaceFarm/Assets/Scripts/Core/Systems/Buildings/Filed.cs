using UnityEngine;

public class Filed : Building
{
    private int _maxStage = 3;
    [SerializeField] private Product _crop;
    [SerializeField] private Product _noneCrope;
    private Field_WindowController _fieldController;

    public override void Start()
    {
        base.Start();
        _fieldController = FindObjectOfType<Field_WindowController>();
    }
    public override void OnClick()
    {
        if (!IsCreated) { return; }

        {
            if (_crop.Type == ResourceType.None) _fieldController.SetFileld(this);
            else if(Progress == _maxStage)
            {
                MainContext.Instance.User.Storage.GetResourceItem(_crop.Type).AddValue(10);

                _crop = _noneCrope;
                Renderer.sprite = null;
            }
        }
    }
    /// <summary>
    /// Вызыввется при создании поля
    /// </summary>
    public override void Initialize(Tile tile)
    {
        base.Initialize(tile);

        GameEvents.Instance.OnGrow.AddListener(GetIncome);
        CurrentTile.SetField(true);
        CurrentTile.SetOccupied(true);
    }
    /// <summary>
    /// Вызыввется при засеивании поля
    /// </summary>
    public void SetCrop(Product crop)
    {
        Progress = 0;
        _crop = crop;
        Renderer.sprite = _crop.Sprites[0];
        _maxStage = _crop.Sprites.Length;
    }
    public override void GetIncome() // Каждые 10 секунд
    {
        if (_crop == null || _crop.Type == ResourceType.None) { return; }

        if (Progress != _maxStage)
        {
            Grow();
        }
    }
    private void Grow()
    {
        Renderer.sprite = _crop.Sprites[Progress];
        Progress++;
    }
}

using UnityEngine;

public class Filed : Building
{
    private int _currentStage = 0, _maxStage = 3;
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
            else if(_currentStage == _maxStage)
            {
                ResourceStorage.Instance.GetResourceItem(_crop.Type).AddValue(10);

                _crop = _noneCrope;
                Renderer.sprite = null;
            }
        }
    }
    /// <summary>
    /// Вызыввется при создании поля
    /// </summary>
    public override void Create(Tile tile)
    {
        base.Create(tile);

        EventBus.Instance.OnGrow.AddListener(GetIncome);
        CurrentTile.SetField(true);
        CurrentTile.SetOccupied(true);
    }
    /// <summary>
    /// Вызыввется при засеивании поля
    /// </summary>
    public void SetCrop(Product crop)
    {
        _currentStage = 0;
        _crop = crop;
        Renderer.sprite = _crop.Sprites[0];
        _maxStage = _crop.Sprites.Length;
    }
    public override void GetIncome() // Каждые 10 секунд
    {
        if (_crop == null || _crop.Type == ResourceType.None) { return; }

        if (_currentStage != _maxStage)
        {
            Grow();
        }
    }
    private void Grow()
    {
        Renderer.sprite = _crop.Sprites[_currentStage];
        _currentStage++;
    }
}

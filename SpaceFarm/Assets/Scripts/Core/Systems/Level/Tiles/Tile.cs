using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Tile : MonoBehaviour
{
    private bool _isOccupied = false;
    private bool _isField = false;

    [SerializeField] public Vector2 SpawnHeight;
    [SerializeField] [Range(0f, 1f)] public float SpawnWeight = 0.5f;

    public TileBehaviour Behaviour;
    public Building TileBuilding;

    public int depth;
    public Sprite FS;

    private const float RAY_DISTANCE = 0.86f;

    private SpriteRenderer _spriteRenderer;

    private Vector3 _defaultSize;

    public void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _defaultSize = this.transform.localScale;

        Create();

        SetBehaviour(Behaviour.GetBehaviour());
    }

    public void SetBehaviour(TileBehaviour behaviour)
    {
        Behaviour = behaviour;
        Behaviour.ParentTile = this;
    }

    public void SetField(bool state)
    {
        _isField = state;

        this.GetComponentInChildren<SpriteRenderer>().sprite = FS;
    }

    public void SetBuilding(Building building)
    {
        TileBuilding = building;
    }

    public void SetOccupied(bool state)
    {
        _isOccupied = state;
    }

    public bool isOccupied()
    {
        return _isOccupied;
    }

    public void SetDepth(int value)
    {
        depth = value;
    }

    virtual public void Create()
    {
        _spriteRenderer.sortingOrder = depth;
    }

    public void ChangeSize(float size)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateSize(size));
    }

    private IEnumerator UpdateSize(float size)
    {
        Vector3 newSize = _defaultSize * size;

        while (this.transform.localScale != newSize)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, newSize, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void OnClick()
    {
        Behaviour.Click();
    }

    public List<Tile> GetNearesrTiles(TileDirection dir)
    {
        List<Tile> result = new List<Tile>();

        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();

        Vector2 origin = new Vector2(this.transform.position.x, this.transform.position.y);
        origin += new Vector2(0, 0.365f);

        if (dir == TileDirection.Up_Right)
        {
            Vector2 dirUpRight = new Vector2(0.5f, 0.5f);
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirUpRight, RAY_DISTANCE));
        }

        if (dir == TileDirection.Up_Left)
        {
            Vector2 dirUpLeft = new Vector2(-0.5f, 0.5f);
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirUpLeft, RAY_DISTANCE));
        }

        if (dir == TileDirection.Down_Right)
        {
            Vector2 dirDownRight = new Vector2(0.5f, -0.5f);
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirDownRight, RAY_DISTANCE));
        }

        if (dir == TileDirection.Down_Left)
        {
            Vector2 dirDownLeft = new Vector2(-0.5f, -0.5f);
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirDownLeft, RAY_DISTANCE));
        }

        if (dir == TileDirection.All)
        {
            Vector2 dirDownLeft = new Vector2(-0.5f, -0.5f);
            Vector2 dirDownRight = new Vector2(0.5f, -0.5f);
            Vector2 dirUpLeft = new Vector2(-0.5f, 0.5f);
            Vector2 dirUpRight = new Vector2(0.5f, 0.5f);

            Debug.DrawLine(origin, dirDownLeft * RAY_DISTANCE, Color.red, 20);
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirDownLeft, RAY_DISTANCE));
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirDownRight, RAY_DISTANCE));
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirUpLeft, RAY_DISTANCE));
            raycastHits.AddRange(Physics2D.RaycastAll(origin, dirUpRight, RAY_DISTANCE));
        }
        
        foreach (var item in raycastHits)
        {
            if (item.transform.GetComponent<Tile>())
            {
                Tile newTile = item.transform.GetComponent<Tile>();

                if (newTile != this)
                {
                    result.Add(newTile);
                }
            }
        }

        return result;
    }
}

public enum TileDirection
{
    Up_Right,
    Up_Left,
    Down_Right,
    Down_Left,
    All
}

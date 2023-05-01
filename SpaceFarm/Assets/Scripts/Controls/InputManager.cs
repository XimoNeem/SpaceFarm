using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool IsActive = true;
    
    [SerializeField] private Camera _camera;
    private Vector3 _mousePosition;
    private Vector3 _camOffset;

    [SerializeField][Range(0.0001f, 0.1f)] private float _sensivity = 0.0001f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (!IsActive) { return; }

        if (Input.touchCount > 0)
        {
            CheckInput(Input.GetTouch(0));
        }

        MoveCamera();
    }

    private void MoveCamera()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        Vector3 currentPos = _camera.transform.position;

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _mousePosition = Input.mousePosition;
            }

            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector3 newPos = Input.touches[0].position;

                _camOffset = _mousePosition - newPos;

                currentPos.x += _camOffset.x * _sensivity;
                currentPos.y += _camOffset.y * _sensivity;

                _mousePosition = Input.touches[0].position;

                _camera.transform.position = currentPos;
            }
        }
    }

    public void CheckInput(Touch touch)
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(touch.position), Vector2.zero);

        if (hit != false)
        {
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (hit.transform.TryGetComponent<Tile>(out Tile tile))
                {
                    tile.OnClick();
                    GameEvents.Instance.OnTileClicked.Invoke(tile);
                }
                else if (hit.transform.TryGetComponent<Building>(out Building building))
                {
                    building.OnClick();
                }
            }
        }
    }
}

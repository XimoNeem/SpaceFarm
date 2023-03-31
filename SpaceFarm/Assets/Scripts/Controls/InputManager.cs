using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool isActive = true;
    public static InputManager instance;
    private const float RAY_DISTANCE = 10;
    private Camera _camera;

    private Vector3 _mousePosition;
    private Vector3 _camOffset;

    [SerializeField][Range(0.0001f, 0.1f)] float _sensivity = 0.0001f;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!isActive) { return; }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            CheckInput(Input.mousePosition);
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            CheckInput(Input.touches[0].position);
        }
#endif

        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 newPos = Input.mousePosition;

            _camOffset = newPos - _mousePosition;

            Vector3 currentPos = _camera.transform.position;

            currentPos.x += _camOffset.x * _sensivity;
            currentPos.y += _camOffset.y * _sensivity;

            _camera.transform.position = currentPos;
            _mousePosition = Input.mousePosition;
        }
    }

    private void CheckInput(Vector3 inputPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit != false)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.transform.gameObject.GetComponent<Tile>())
                {
                    hit.transform.gameObject.GetComponent<Tile>().OnClick();
                    EventBus.Instance.OnTileClicked.Invoke(hit.transform.gameObject.GetComponent<Tile>());
                }
                else if (hit.transform.gameObject.GetComponent<Building>())
                {
                    hit.transform.gameObject.GetComponent<Building>().OnClick();
                }
            }
        }
    }
}

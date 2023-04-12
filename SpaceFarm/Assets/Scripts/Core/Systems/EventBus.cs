using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public UnityEvent OnResourcesChanged;
    public UnityEvent OnGrow;
    public UnityEvent OnMove;
    public UnityEvent<Tile> OnTileClicked;
    

    public static EventBus Instance;

    private ResourceStorage _ResourceStorage;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }

        StartCoroutine(Timer_Grow());
        StartCoroutine(Timer_Move());
    }

    private IEnumerator Timer_Grow()
    {
        while (true)
        {
            OnGrow.Invoke();
            yield return new WaitForSecondsRealtime(10);
        }
    }

    private IEnumerator Timer_Move()
    {
        while (true)
        {
            OnMove.Invoke();
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public UnityEvent OnResourcesChanged;
    public UnityEvent OnTime;
    public UnityEvent<Tile> OnTileClicked;
    

    public static EventBus Instance;

    private ResourceStorage _ResourceStorage;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }

        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
           OnTime.Invoke();
            yield return new WaitForSecondsRealtime(10);
        }
    }
}

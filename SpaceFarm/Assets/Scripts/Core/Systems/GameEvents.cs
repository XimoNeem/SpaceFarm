using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public UnityEvent OnResourcesChanged;
    public UnityEvent OnMainLoaded;
    public UnityEvent OnGrow;
    public UnityEvent OnMove;
    public UnityEvent<Tile> OnTileClicked;

    public static GameEvents Instance;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }

        DontDestroyOnLoad(this);

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

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoad;
    }

    public void LoadScene(string name)
    {
        StartCoroutine(Load(name));
    }

    private IEnumerator Load(string name)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(name);
        loader.allowSceneActivation = false;
        while (!loader.isDone)
        {
            yield return new WaitForEndOfFrame();
            if (loader.progress == 0.9f)
            {
                loader.allowSceneActivation = true;
            }
        }
    }

    private void OnLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == Game.Data.Scenes.MAIN)
        {
            GameEvents.Instance.OnMainLoaded.Invoke();
        }
    }
}
using System.Collections;
using UnityEngine;
using Game.Data;

public class MainContext : MonoBehaviour
{
    public static MainContext Instance;

    public UserData User;
    public ResourceStorage Storage;
    public BuildSystem BuildSystem;
    public LevelGenerator LevelGenerator;
    public InputManager InputManager;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {

    }
}
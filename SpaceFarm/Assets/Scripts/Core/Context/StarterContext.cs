using UnityEngine;
using Game.Data;

public class StarterContext : MonoBehaviour
{
    public static StarterContext Instance;

    public Login_WindowController Login_WindowController;
    public LoginSystem LoginSystem;

    private void Awake()
    {
        Instance = this;
    }
}
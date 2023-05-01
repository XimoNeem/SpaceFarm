using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Networking;
using Game.Data;

public class LoginSystem : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (GetKeys())
        {
            StartCoroutine(DataBaseHandler.GetUser
                (
                    PlayerPrefs.GetString("saved_login"),
                    PlayerPrefs.GetString("saved_password"),
                    LoadGame,
                    StarterContext.Instance.Login_WindowController.ShowWarning
                ));
        }
        else
        {
            StarterContext.Instance.Login_WindowController.ShowLoadingScreen(false);
        }
    }

    public void SaveLoginData(string login, string password)
    {
        PlayerPrefs.SetString("saved_login", login);
        PlayerPrefs.SetString("saved_password", password);
        PlayerPrefs.Save();
    }

    public bool GetKeys()
    {
        if (PlayerPrefs.HasKey("saved_login") && PlayerPrefs.HasKey("saved_password")) { return true; }
        else { return false; }
    }

    public void SignIn(string email, string password)
    {
        StartCoroutine(DataBaseHandler.GetUser(email, password, LoadGame, StarterContext.Instance.Login_WindowController.ShowWarning));
    }

    public void LoadGame(UserData data, ResourceStorage storage)
    {
        if (!GetKeys()) { SaveLoginData(data.Email, data.Password); }

        MainContext.Instance.User = data;
        MainContext.Instance.Storage = storage;
        SceneLoader.Instance.LoadScene(Scenes.MAIN);
    }

    public void SignUp(string name, string email, string password)
    {
        StartCoroutine(DataBaseHandler.CreateUser
            (
                name,
                email,
                password,
                LoadGame,
                StarterContext.Instance.Login_WindowController.ShowWarning
            ));
    }
}
using System.Collections;
using UnityEngine;
using TMPro;
using Game.Networking;
using Game.Data;
using UnityEngine.SceneManagement;

public class Login_WindowController : MonoBehaviour
{
    [SerializeField] private GameObject[] _loginItems, _registerItems;
    [SerializeField] private Transform _itemsParent;

    [SerializeField] private GameObject _warningWindow, _loadingWindow;
    [SerializeField] private TMP_Text _warningText;

    [SerializeField] private TMP_InputField _loginEmail, _loginPassword;
    [SerializeField] private TMP_InputField _registerEmail, _registerPassword, _registerPasswordCheck, _registerName;

    private void Start()
    {
        if (GetKeys())
        {
            StartCoroutine(DataBaseHandler.GetUser(PlayerPrefs.GetString("saved_login"), PlayerPrefs.GetString("saved_password"), LoadGame, ShowWarning));
        }
        else
        {
            _loadingWindow.SetActive(false);
        }
    }

    public void SignUp()
    {
        if (_registerEmail.text == "" || _registerName.text == "" || _registerPassword.text == "" || _registerPasswordCheck.text == ""){ return; }
        if (_registerPassword.text != _registerPasswordCheck.text)
        {
            ShowWarning("Password mismatch");
            return;
        }

        _loadingWindow.SetActive(true);

        SignUpData data = new SignUpData();
        data.Email = _registerEmail.text;
        data.Name = _registerName.text;
        data.Password = _registerPassword.text;

        StartCoroutine(DataBaseHandler.CreateUser(data, LoadGame, ShowWarning));
    }

    private void LoadGame(UserData data)
    {
        if (!GetKeys()) { SaveLoginData(data.Email, data.Password); }

        User.Data = data;
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        _loadingWindow.SetActive(true);
        AsyncOperation loader = SceneManager.LoadSceneAsync(1);
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

    private void SaveLoginData(string login, string password)
    {
        PlayerPrefs.SetString("saved_login", login);
        PlayerPrefs.SetString("saved_password", password);
        PlayerPrefs.Save();
    }

    public void SignIn()
    {
        if (_loginEmail.text == "" || _loginPassword.text == "") { return; }

        _loadingWindow.SetActive(true);
        StartCoroutine(DataBaseHandler.GetUser(_loginEmail.text, _loginPassword.text, LoadGame, ShowWarning));
    }

    public void SetSignUp()
    {
        _itemsParent.position += new Vector3(0, 500, 0);
        foreach (var item in _loginItems)
        {
            item.SetActive(false);
        }
        foreach (var item in _registerItems)
        {
            item.SetActive(true);
        }
    }

    public void SetSignIn()
    {
        _itemsParent.position += new Vector3(0, 500, 0);
        foreach (var item in _loginItems)
        {
            item.SetActive(true);
        }
        foreach (var item in _registerItems)
        {
            item.SetActive(false);
        }
    }

    private void ShowWarning(string message)
    {
        _loadingWindow.SetActive(false);
        _warningWindow.SetActive(true);
        _warningText.text = message;
    }

    private bool GetKeys()
    {
        if (PlayerPrefs.HasKey("saved_login") && PlayerPrefs.HasKey("saved_password")) { return true; }
        else { return false; }
    }
}

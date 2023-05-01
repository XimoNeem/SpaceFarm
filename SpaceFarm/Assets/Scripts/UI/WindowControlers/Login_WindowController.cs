using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    [SerializeField] private Button _signInButton, _signUpButton, _switchToSignIn, _switchToSignUp;

    private void OnEnable()
    {
        _switchToSignIn.onClick.AddListener(ShowSignIn);
        _switchToSignUp.onClick.AddListener(ShowSignUp);
        _signInButton.onClick.AddListener(SignIn);
        _signUpButton.onClick.AddListener(SignUp);
    }

    private void SignUp()
    {
        if (_registerEmail.text == "" || _registerName.text == "" || _registerPassword.text == "" || _registerPasswordCheck.text == ""){ return; }
        if (_registerPassword.text != _registerPasswordCheck.text)
        {
            ShowWarning("Password mismatch");
            return;
        }

        ShowLoadingScreen(true);
        StarterContext.Instance.LoginSystem.SignUp
            (
                _registerName.text,
                _registerEmail.text,
                _registerPassword.text
            );
    }

    private void SignIn()
    {
        if (_loginEmail.text == "" || _loginPassword.text == "") { return; }

        ShowLoadingScreen(true);
        StarterContext.Instance.LoginSystem.SignIn
            (
                _loginEmail.text,
                _loginPassword.text
            );
    }

    private void ShowSignUp()
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

    private void ShowSignIn()
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

    public void ShowWarning(string message)
    {
        ShowLoadingScreen(false);
        _warningWindow.SetActive(true);
        _warningText.text = message;
    }

    public void ShowLoadingScreen(bool state)
    {
        _loadingWindow.SetActive(state);
    }
}

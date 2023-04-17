using UnityEngine;
using TMPro;
using Game.Networking;
using Game.Data;

public class Login_WindowController : MonoBehaviour
{
    [SerializeField] private GameObject[] _loginItems, _registerItems;
    [SerializeField] private Transform _itemsParent;

    [SerializeField] private GameObject _warningWindow, _loadingWindow;
    [SerializeField] private TMP_Text _warningText;

    [SerializeField] private TMP_InputField _loginEmail, _loginPassword;

    private void Start()
    {
        
    }

    public void SignUp()
    {
        
    }

    public void SignIn()
    {
        Debug.Log(_loginEmail.text);
        StartCoroutine(DataBaseHandler.GetUser(_loginEmail.text, _loginPassword.text, testUser, ShowWarning));
    }

    public void testUser(UserData user)
    {
        Debug.Log(user.Name);
    }

    public void SetSignUp()
    {
        ShowWarning("Test");
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
        ShowWarning("Test");
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
        _warningWindow.SetActive(true);
        _warningText.text = message;
    }
}

using System.Collections;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetServer());
    }

    private IEnumerator GetServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("MESSAGE", "Hello");
        WWW www = new WWW("http://nevile.pluton-host.ru/gamedata/", form);
        yield return www;
        if (www.error != null)
        {
            Debug.Log("Error " + www.error);
            yield break;
        }
        Debug.Log("answer " + www.text);
        yield break;
    }
}

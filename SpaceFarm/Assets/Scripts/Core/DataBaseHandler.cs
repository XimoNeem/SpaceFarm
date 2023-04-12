using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Game.Data;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Networking
{
    public static class DataBaseHandler
    {
        public static IEnumerator CheckConnection(Action successCallback, Action errorCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("CHECK_CONNECTION", "test");

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/", form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    errorCallback.Invoke();
                    yield break;
                }
                else
                {
                    successCallback.Invoke();
                }
            }
        }

        public static IEnumerator CreateUser(SingUpData data, Action successCallback, Action<string> errorCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("NAME", data.Name);
            form.AddField("EMAIL", data.Email);
            form.AddField("PASSWORD", data.Password);

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/create_user.php", form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    errorCallback.Invoke(request.error);
                    yield break;
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                    successCallback.Invoke();
                }
            }
        }

        public static IEnumerator GetUser(int ID, Action<UserData> callback)
        {
            WWWForm form = new WWWForm();
            form.AddField("GET_USER", ID.ToString());

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/get_user.php", form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    yield break;
                }
                else
                {
                    string jsonText = request.downloadHandler.text;
                    Debug.Log(jsonText);
                }
            }
        }

        public static IEnumerator SaveUser(UserData data, Action successCallback, Action<string> errorCallback)
        {
            string resourcesJson = JsonUtility.ToJson(data.Resources);

            WWWForm form = new WWWForm();
            form.AddField("ID", data.ID);
            form.AddField("NAME", data.Name);
            form.AddField("RESOURCES", resourcesJson);

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/save_user.php", form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    errorCallback.Invoke(request.error);
                    yield break;
                }
                else
                {
                    successCallback.Invoke();
                }
            }
        }

        public static IEnumerator LoadUser(int id, Action<UserData> successCallback, Action<string> errorCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("ID", id);

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/load_user.php", form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    errorCallback.Invoke(request.error);
                    yield break;
                }
                else
                {
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);

                    StorageInfo resources = JsonUtility.FromJson<StorageInfo>(values["user_resources"]);


                    UserData data = new UserData(
                            id,
                            values["user_name"],
                            values["user_email"],
                            values["user_password"]
                        );
                    data.Resources = resources;

                    successCallback.Invoke(data);
                }
            }
        }
    }
}
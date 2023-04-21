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

        public static IEnumerator CreateUser(SignUpData data, Action<UserData> successCallback, Action<string> errorCallback)
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
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);
                    UserData result = new UserData
                        (
                            int.Parse(values["user_id"]),
                            data.Name,
                            data.Email,
                            data.Password
                        );

                    successCallback.Invoke(result);
                }
            }
        }

        public static IEnumerator GetUser(string email, string password, Action<UserData> successCallback, Action<string> errorCallback)
        {
            WWWForm form = new WWWForm();
            form.AddField("GET_USER_EMAIL", email);
            form.AddField("GET_USER_PASSWORD", password);

            using (UnityWebRequest request = UnityWebRequest.Post("http://nevile.pluton-host.ru/gamedata/get_user.php", form))
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
                    Debug.LogError(request.downloadHandler.text);
                    if (request.downloadHandler.text.StartsWith("[ERROR]"))
                    {
                        errorCallback.Invoke(request.downloadHandler.text.Replace("[ERROR]", ""));
                        yield break;
                    }
                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(request.downloadHandler.text);


                    StorageInfo resources = JsonUtility.FromJson<StorageInfo>(values["user_resources"]);

                    UserData data = new UserData(
                            int.Parse(values["user_id"]),
                            values["user_name"],
                            values["user_email"],
                            values["user_password"]
                        );

                    successCallback.Invoke(data);
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

        public static IEnumerator LoadUserData(int id, Action<UserData> successCallback, Action<string> errorCallback)
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
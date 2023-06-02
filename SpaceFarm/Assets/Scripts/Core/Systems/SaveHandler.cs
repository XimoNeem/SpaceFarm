using Game.Networking;
using UnityEngine;
public class SaveHandler : MonoBehaviour
{
    private void Start()
    {
        GameEvents.Instance.OnResourcesChanged.AddListener(SaveUserData);
    }

    private void SaveUserData()
    {
        MainContext.Instance.User.LevelData.Buildings.Clear();

        foreach (var item in FindObjectsOfType<Building>())
        {
            MainContext.Instance.User.LevelData.Buildings.Add(item.CurrentTile.Index, new Game.Data.BuildingSaveItem(item.ID, item.Progress));
        }

        StartCoroutine(DataBaseHandler.SaveUser(MainContext.Instance.User, Done, Error));
    }
    public void Error(string message)
    {
        
    }
    public void Done()
    {

    }
}

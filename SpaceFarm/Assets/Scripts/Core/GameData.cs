using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public static class User
    {
        public static UserData Data;
    }
    public class SignUpData
    {
        public string Name;
        public string Email;
        public string Password;
    }

    public class UserData
    {
        public int ID;
        public string Name;
        public string Email;
        public string Password;
        public StorageInfo Resources;
        public LevelData LevelData;

        public UserData(int id, string name, string email, string password)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
        }
        public override string ToString()
        {
            return $"User info : {ID} {Name} {Email} {Password}";
        }
    }

    public class LevelData
    {
        public List<Tile> Tiles;
    }

    [System.Serializable]
    public class StorageInfo
    {
        public ResourceItem[] resources;
        public ResourceItem[] crops;
    }
}

namespace Game.Exeptions
{
    [System.Serializable]
    public class NoSuchResourceExeption : System.Exception
    {
        
    }
}
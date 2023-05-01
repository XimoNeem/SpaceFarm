using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
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

    public class Scenes
    {
        public const string LOADER = "Starter";
        public const string MAIN = "Main";
    }
}

namespace Game.Exeptions
{
    [System.Serializable]
    public class NoSuchResourceExeption : System.Exception
    {
        
    }
}
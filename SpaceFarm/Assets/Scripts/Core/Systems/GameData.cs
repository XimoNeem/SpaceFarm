using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public class LevelData
    {
        public float NoiseScale;
        public Dictionary<int, BuildingSaveItem> Buildings;

        public LevelData()
        {
            Buildings = new Dictionary<int, BuildingSaveItem>();
        }
    }

    [System.Serializable]
    public class StorageInfo
    {
        public ResourceItem[] resources;
        public ResourceItem[] crops;
    }

    [System.Serializable]
    public class BuildingSaveItem
    {
        public int ID;
        public int Progress;

        public BuildingSaveItem(int iD, int progress)
        {
            ID = iD;
            Progress = progress;
        }
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
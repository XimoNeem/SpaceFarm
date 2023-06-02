namespace Game.Data
{
    [System.Serializable]
    public class UserData
    {
        public int ID;
        public string Name;
        public string Email;
        public string Password;
        public ResourceStorage Storage;
        public LevelData LevelData;

        public UserData(int id, string name, string email, string password)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;

            LevelData = new LevelData();


            GameEvents.Instance.OnMainLoaded.AddListener(CreateLevel);
        }
        public override string ToString()
        {
            return $"User info : {ID} {Name} {Email} {Password}";
        }

        public void CreateLevel()
        {
            MainContext.Instance.LevelGenerator.GenerateLevel();
        }
    }
}
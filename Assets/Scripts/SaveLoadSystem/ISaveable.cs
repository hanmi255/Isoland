namespace Assets.Scripts.SaveLoadSystem
{
    public interface ISaveable
    {
        public void SaveableRegister()
        {
            SaveLoadManager.Instance.Register(this);
        }

        public GameSaveData GenerateSaveData();

        public void RestoreGameData(GameSaveData saveData);
    }
}


namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        // Ваши сохранения

        public double PlayerCoins = 0.0;
        public double PlayerUranium = 0.0;
        public int[] PlayerUpgradesLevels = new int[20];
        public double PlayerCoinPerClick = 0;
        public double PlayerCoinPerAutoClick = 0;
    }
}

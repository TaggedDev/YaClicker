
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

        public double[] PlayerResourceValues { get; } = { 0.0, 0.0 };
        public int[] PlayerUpgradesLevels { get; } = new int[20];
    }
}

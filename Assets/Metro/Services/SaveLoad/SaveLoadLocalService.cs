using System.Threading.Tasks;
using UnityEngine;
using Metro.Data;
using Metro.Services.PersistentData;

using static Newtonsoft.Json.JsonConvert;

namespace Metro.Services.SaveLoad
{
    public class SaveLoadLocalService : ISaveLoadService
    {
        private const string PROGRESS_KEY = "Progress";
        private const string SETTINGS_KEY = "Settings";

        private readonly IPersistentDataService _persistentDataService;

        public SaveLoadLocalService(IPersistentDataService persistentDataService)
        {
            _persistentDataService = persistentDataService;
        }

        public void SaveProgress()
        {
            var progress = SerializeObject(_persistentDataService.Progress);
            PlayerPrefs.SetString(PROGRESS_KEY, progress);
        }

        public Task<PlayerProgressData> LoadProgress()
        {
            var progress = DeserializeObject<PlayerProgressData>(PlayerPrefs.GetString(PROGRESS_KEY));
            return Task.FromResult(progress);
        }

        public void SaveSettings()
        {
            var settings = SerializeObject(_persistentDataService.Settings);
            PlayerPrefs.SetString(SETTINGS_KEY, settings);
        }

        public Task<PlayerSettingsData> LoadSettings()
        {
            var settings = DeserializeObject<PlayerSettingsData>(PlayerPrefs.GetString(SETTINGS_KEY));
            return Task.FromResult(settings);
        }
    }
}
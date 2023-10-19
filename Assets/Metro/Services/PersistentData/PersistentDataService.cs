using Metro.Data;

namespace Metro.Services.PersistentData
{
    public class PersistentDataService : IPersistentDataService
    {
        public PlayerSettingsData Settings { get; set; }
        public PlayerProgressData Progress { get; set; }
    }
}
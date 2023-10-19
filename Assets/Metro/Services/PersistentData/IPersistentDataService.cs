using Metro.Data;

namespace Metro.Services.PersistentData
{
    public interface IPersistentDataService
    {
        PlayerSettingsData Settings { get; set; }
        PlayerProgressData Progress { get; set; }
    }
}
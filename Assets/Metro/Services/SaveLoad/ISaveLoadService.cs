using System.Threading.Tasks;
using Metro.Data;

namespace Metro.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        Task<PlayerProgressData> LoadProgress();

        void SaveSettings();
        Task<PlayerSettingsData> LoadSettings();
    }
}
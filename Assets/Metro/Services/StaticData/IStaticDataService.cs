using System.Collections.Generic;
using System.Threading.Tasks;
using Metro.StaticData;
using Metro.StaticData.Player;

namespace Metro.Services.StaticData
{
    public interface IStaticDataService
    {
        Task Initialize();
        
        LevelStaticData ForLevel(string levelKey);
        List<LevelStaticData> GetAllLevels { get; }
        
        public PlayerStaticData ForPlayer();
        public void ForWindow();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Metro.Infrastructure.AssetManagement;
using Metro.Services.Logging;
using Metro.StaticData;
using Metro.StaticData.Player;

namespace Metro.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelsList    = "LevelsList";
        private const string ItemsList     = "ItemsList";
        private const string EnemiesList   = "EnemiesList";
        private const string PlayerConfig = "PlayerConfig";
        
        private readonly ILoggingService _logger;
        private readonly IAssetProvider _assetProvider;

        private Dictionary<string, LevelStaticData> _levels;
        private PlayerStaticData _player;


        public StaticDataService(ILoggingService logger, IAssetProvider assetProvider)
        {
            _logger = logger;
            _assetProvider = assetProvider;
        }

        public async Task Initialize()
        {
            await Task.WhenAll(
                LoadLevels(),
                LoadPlayer()
            );
        }

        public LevelStaticData ForLevel(string levelKey) =>
            _levels.TryGetValue(levelKey, out var levelStaticData)
                ? levelStaticData
                : null;

        public List<LevelStaticData> GetAllLevels =>
            _levels.Values.ToList();

        public PlayerStaticData ForPlayer() => 
            _player;

        public void ForWindow() => 
            throw new NotImplementedException();

        
        private async Task LoadLevels()
        {
            var list = await _assetProvider.Load<LevelsList>(key: LevelsList);
            
            _levels = list
                .levels
                .ToDictionary(x => x.Key, x => x);
        }
        
        private async Task LoadPlayer()
        {
            _player = await _assetProvider.Load<PlayerStaticData>(key: PlayerConfig);
        }
    }
}

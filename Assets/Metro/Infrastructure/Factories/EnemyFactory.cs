using System.Threading.Tasks;
using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Services.StaticData;
using Metro.StaticData.Enemies;
using UnityEngine;
using Zenject;

namespace Metro.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemyPrefabId = "EnemyPrefab";
        
        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        
        public EnemyFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }
        
        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(key: EnemyPrefabId);
        }

        public void CleanUp()
        {
            _assetProvider.Release(key: EnemyPrefabId);
        }

        public Task<GameObject> Create(EnemyType enemyType, Vector2 at)
        {
            throw new System.NotImplementedException();
        }
    }
}
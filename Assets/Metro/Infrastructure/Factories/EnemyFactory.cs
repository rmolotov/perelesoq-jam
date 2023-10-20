using System;
using System.Threading.Tasks;
using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Services.StaticData;
using Metro.StaticData.Enemies;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Metro.Infrastructure.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private const string EnemyPrefabPrefix = "Enemy";
        
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
            foreach (var enemyType in (EnemyType[]) Enum.GetValues(typeof(EnemyType)))
                await _assetProvider.Load<GameObject>(key: EnemyPrefabPrefix + enemyType);
        }

        public void CleanUp()
        {
            foreach (var enemyType in (EnemyType[]) Enum.GetValues(typeof(EnemyType)))
                _assetProvider.Release(key: EnemyPrefabPrefix + enemyType);
        }

        public async Task<GameObject> Create(EnemyStaticData enemyStaticData)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: EnemyPrefabPrefix + enemyStaticData.EnemyType);
            var position =
                Vector3.forward * (enemyStaticData.Position + 0.5f) +
                (enemyStaticData.SpawnSide == EnemySide.Right ? Vector3.right : Vector3.left) * 1.5f;
            var enemy = Object.Instantiate(prefab, position, Quaternion.identity);

            _container.InjectGameObject(enemy);

            return enemy;
        }
    }
}
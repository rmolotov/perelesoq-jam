using System.Threading.Tasks;
using Metro.Gameplay.Train;
using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Services.StaticData;
using UnityEngine;
using Zenject;

namespace Metro.Infrastructure.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private const string TrainBasePrefabId      = "TrainBasePrefab";
        private const string TrainModulePrefabId    = "TrainModulePrefab";
        private const int ModuleOffset = 7;

        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        public LevelFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(key: TrainBasePrefabId);
            await _assetProvider.Load<GameObject>(key: TrainModulePrefabId);
        }

        public void CleanUp()
        {
            _assetProvider.Release(key: TrainBasePrefabId);
            _assetProvider.Release(key: TrainModulePrefabId);
        }

        public async Task<TrainController> Create(int length)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: TrainBasePrefabId);
            var train = Object.Instantiate(prefab).GetComponent<TrainController>();
            
            _container.Inject(train);

            var modulePrefab = await _assetProvider.Load<GameObject>(key: TrainModulePrefabId);
            for (var i = 0; i < length; i++)
                Object.Instantiate(
                    modulePrefab,
                    train.transform.position + Vector3.forward * (i * ModuleOffset),
                    Quaternion.identity,
                    train.transform);

            train.Initialize(length * ModuleOffset);
            
            return train;
        }
    }
}
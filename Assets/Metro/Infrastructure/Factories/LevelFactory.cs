using System.Threading.Tasks;
using Metro.Gameplay.Conductor;
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
        private const string ConductorPrefabId      = "ConductorPrefab";
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

        public TrainController Train { get; private set; }

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
            Train = Object.Instantiate(prefab).GetComponent<TrainController>();
            
            _container.Inject(Train);

            var modulePrefab = await _assetProvider.Load<GameObject>(key: TrainModulePrefabId);
            for (var i = 0; i < length; i++)
                Object.Instantiate(
                    modulePrefab,
                    Train.transform.position + Vector3.forward * (i * ModuleOffset),
                    Quaternion.identity,
                    Train.transform);
            
            var conductorPrefab = await _assetProvider.Load<GameObject>(key: ConductorPrefabId);
            var conductor = Object.Instantiate(conductorPrefab).GetComponent<ConductorMove>();
            
            _container.InjectGameObject(conductor.gameObject);

            Train.Initialize(length * ModuleOffset, conductor);
            
            return Train;
        }
    }
}
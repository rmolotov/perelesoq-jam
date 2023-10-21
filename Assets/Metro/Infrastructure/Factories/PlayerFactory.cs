using System.Threading.Tasks;
using UnityEngine;
using JetBrains.Annotations;
using Zenject;
using Metro.Gameplay.Player;
using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Services.StaticData;
using Object = UnityEngine.Object;

namespace Metro.Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private const string PlayerPrefabId = "PlayerPrefab";

        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        
        [CanBeNull] public PlayerController Player { get; private set; }

        public PlayerFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(key: PlayerPrefabId);
        }

        public void CleanUp()
        {
            Player = null;
            _assetProvider.Release(key: PlayerPrefabId);
        }

        public async Task<PlayerController> Create(Vector3 at)
        {
            var config = _staticDataService.ForPlayer();
            var prefab = await _assetProvider.Load<GameObject>(key: PlayerPrefabId);
            var playerGameObject = Object.Instantiate(prefab, at, Quaternion.identity);
            
            _container.InjectGameObject(playerGameObject);

            Player = playerGameObject.GetComponent<PlayerController>();
            Player.Initialize(config);

            return Player;
        }
    }
}
using System.Threading.Tasks;
using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Meta.HUD;
using Metro.Meta.Menu;
using Metro.Services.StaticData;
using Metro.StaticData;
using Metro.StaticData.Levels;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Metro.Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPrefabId       = "UIRootPrefab";
        private const string HudPrefabId          = "HudPrefab";
        private const string MenuPrefabId         = "MainMenuPrefab";
        private const string LevelCardPrefabId    = "LevelCardPrefab";

        private readonly DiContainer _container;
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private Canvas _uiRoot;

        public UIFactory(
            DiContainer container, 
            IAssetProvider assetProvider,
            IStaticDataService staticDataService
        )
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(key: UIRootPrefabId);
            await _assetProvider.Load<GameObject>(key: HudPrefabId);
            await _assetProvider.Load<GameObject>(key: MenuPrefabId);
        }

        public void CleanUp()
        {
            _assetProvider.Release(key: MenuPrefabId);
            _assetProvider.Release(key: LevelCardPrefabId);
        }

        public async Task<Canvas> CreateUIRoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: UIRootPrefabId);
            return _uiRoot = Object.Instantiate(prefab).GetComponent<Canvas>();
        }

        public async Task<HUDController> CreateHud()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: HudPrefabId);
            var hud = Object
                .Instantiate(prefab, _uiRoot.transform)
                .GetComponent<HUDController>();

            _container.Inject(hud);
            return hud;
        }

        public async Task<MenuController> CreateMainMenu()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: MenuPrefabId);
            var menu = Object.Instantiate(prefab, _uiRoot.transform).GetComponent<MenuController>();

            foreach (var levelData in _staticDataService.GetAllLevels) 
                await CreateStageCard(levelData, menu);

            _container.InjectGameObject(menu.gameObject);
            return menu;
        }

        private async Task<LevelCard> CreateStageCard(LevelStaticData stageStaticData, MenuController menu)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: LevelCardPrefabId);
            // var sprite = await _assetProvider.Load<Sprite>(key: stageStaticData.Key);
            var card = Object.Instantiate(prefab, menu.levelTogglesContainer.transform).GetComponent<LevelCard>();

            card.OnSelect += st => menu.SelectedStage.Value = st;
            // card.Initialize(stageStaticData, sprite, menu.stagesTogglesContainer);
            card.Initialize(stageStaticData, menu.levelTogglesContainer);

            return card;
        }
    }
}
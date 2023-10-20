using System.Collections.Generic;
using System.Threading.Tasks;
using Metro.Gameplay;
using Metro.Gameplay.Player;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Infrastructure.SceneManagement;
using Metro.Infrastructure.States.Interfaces;
// using Metro.Services.Camera;
// using Metro.Services.Curtain;
// using Metro.Gameplay.Player;
// using Metro.Gameplay.Camera.CinemachineExtensions;
using Metro.StaticData;
using Metro.StaticData.Levels;

namespace Metro.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelStaticData>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        private readonly IUIFactory _uiFactory;
        private readonly ILevelFactory _levelFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnemyFactory _enemyFactory;
        
        // private readonly ICameraService _cameraService;
        // private readonly ICurtainService _curtain;

        private LevelStaticData _pendingLevelStaticData;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            IPlayerFactory playerFactory,
            ILevelFactory levelFactory,
            IEnemyFactory enemyFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            
            _uiFactory = uiFactory;
            _levelFactory = levelFactory;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
        }

        private Canvas _uiRoot;

        public async void Enter(LevelStaticData levelStaticData)
        {
            _pendingLevelStaticData = levelStaticData;
            
            // _curtain.Show(0.5f);

            await Task.WhenAll(
                _levelFactory.WarmUp(),
                _playerFactory.WarmUp(),
                _enemyFactory.WarmUp()
            );
            
            var sceneInstance = await _sceneLoader.Load(SceneName.Game, OnLoaded);
        }

        public void Exit()
        {
            // _curtain.Hide(0.5f);
            _levelFactory.CleanUp();
            _playerFactory.CleanUp();
            _enemyFactory.CleanUp();
            _pendingLevelStaticData = null;
        }

        private async void OnLoaded(SceneName sceneName)
        {
            await Task.WhenAll(
                InitUIRoot(),
                InitGameWold(),
                InitUI(),
                InitPlayer()
            );
            
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitUIRoot()
        {
            _uiRoot = await _uiFactory.CreateUIRoot();
            _uiRoot.enabled = false;
        }

        private async Task InitGameWold()
        {
            var train = await _levelFactory.Create(_pendingLevelStaticData.Length);

            foreach (var enemyStaticData in _pendingLevelStaticData.Enemies)
                await _enemyFactory.Create(enemyStaticData);
        }

        private async Task InitUI()
        {
            await _uiFactory
                .CreateHud()
                .ContinueWith(
                    m => m.Result.Initialize(_pendingLevelStaticData),
                    TaskScheduler.FromCurrentSynchronizationContext());

            _uiRoot.enabled = true;
        }

        private async Task InitPlayer()
        {
            var player = await SetupPlayer();
            SetupCamera(player);
        }

        private async Task<PlayerController> SetupPlayer()
        {
            return await _playerFactory.Create(at: _pendingLevelStaticData.PlayerSpawnPoint);
        }

        private void SetupCamera(PlayerController player)
        {
            // _cameraService.GetPlayerVCamera().Follow = player.transform;
        }
    }
}
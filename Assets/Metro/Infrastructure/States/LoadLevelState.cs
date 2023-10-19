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

namespace Metro.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelStaticData>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;
        // private readonly ICameraService _cameraService;
        // private readonly ICurtainService _curtain;

        private LevelStaticData _pendingLevelStaticData;

        public LoadLevelState(
            GameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            IPlayerFactory playerFactory,
            IUIFactory uiFactory)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
        }

        private Canvas _uiRoot;
        
        public async void Enter(LevelStaticData levelStaticData)
        {
            _pendingLevelStaticData = levelStaticData;
            
            // _curtain.Show(0.5f);
            
            await _playerFactory.WarmUp();
            // await _stageFactory.WarmUp();

            var sceneInstance = await _sceneLoader.Load(SceneName.Game, OnLoaded);
        }

        public void Exit()
        {
            // _curtain.Hide(0.5f);
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
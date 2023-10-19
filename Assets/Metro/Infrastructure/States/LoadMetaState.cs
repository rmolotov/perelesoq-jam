using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Infrastructure.SceneManagement;
using Metro.Infrastructure.States.Interfaces;

namespace Metro.Infrastructure.States
{
    public class LoadMetaState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly SceneLoader _sceneLoader;
        
        private SceneInstance _sceneInstance;

        public LoadMetaState(
            GameStateMachine stateMachine, 
            IUIFactory uiFactory,
            SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            // TODO: show curtain
            _sceneInstance = await _sceneLoader.Load(SceneName.Meta, OnLoaded);
        }

        public void Exit()
        {
            _uiFactory.CleanUp();
        }
        
        private async void OnLoaded(SceneName sceneName)
        {
            await InitUIRoot();
            await InitMainMenu();
        }

        private async Task InitUIRoot() => 
            await _uiFactory.CreateUIRoot();

        private async Task InitMainMenu() =>
            await _uiFactory
                .CreateMainMenu()
                .ContinueWith(
                    m => m.Result.Initialize(),
                    TaskScheduler.FromCurrentSynchronizationContext());
    }
}
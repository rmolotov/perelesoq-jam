using Zenject;
using Metro.Infrastructure.States;

namespace Metro.Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadProgressState>().AsSingle().NonLazy();
            Container.Bind<LoadMetaState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle(); // GameStateMachine entry point is Initialize()
        }
    }
}
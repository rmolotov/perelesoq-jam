using UnityEngine;
using Zenject;

using Metro.Infrastructure.AssetManagement;
using Metro.Infrastructure.Factories;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Infrastructure.SceneManagement;
using Metro.Services.Input;
using Metro.Services.Logging;
using Metro.Services.PersistentData;
using Metro.Services.SaveLoad;
using Metro.Services.StaticData;


namespace Metro.Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private GameObject curtainServicePrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressableProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();

            BindServices();
            BindFactories();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<LoggingService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PersistentDataService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveLoadLocalService>().AsSingle().NonLazy();

            // Container.BindInterfacesAndSelfTo<LevelProgressServiceResolver>()
            //     .AsSingle()
            //     .CopyIntoDirectSubContainers();
            // Container.BindInterfacesAndSelfTo<LevelProgressService>().AsSingle().NonLazy();

            // Container.BindInterfacesAndSelfTo<CurtainService>()
            //     .FromComponentInNewPrefab(curtainServicePrefab)
            //     .WithGameObjectName("Curtain")
            //     .UnderTransformGroup("Infrastructure")
            //     .AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            // Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
        }
    }
}
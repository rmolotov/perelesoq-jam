using System;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Infrastructure.States;
using Metro.Services.Logging;
using Metro.Services.PersistentData;
using Metro.Services.SaveLoad;
using Metro.StaticData;
using Metro.StaticData.Levels;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Metro.Meta.Menu
{
    public class MenuController : MonoBehaviour
    {
        public readonly ReactiveProperty<LevelStaticData> SelectedStage = new();
        public ToggleGroup levelTogglesContainer;

        [SerializeField] private Button startLevelButton;
        [SerializeField] private Button settingsButton;
        // [SerializeField] private WindowBase settingsWindow;

        private GameStateMachine _stateMachine;
        private ILoggingService _logger;
        private IUIFactory _uiFactory;
        private IPersistentDataService _persistentDataService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(
            GameStateMachine stateMachine,
            ILoggingService loggingService,
            IUIFactory uiFactory,
            IPersistentDataService persistentDataService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _logger = loggingService;
            _uiFactory = uiFactory;
            _persistentDataService = persistentDataService;
            _saveLoadService = saveLoadService;
        }

        public async void Initialize()
        {
            SetupButtons();
            
            _logger.LogMessage("initialized", this);
        }

        private void SetupButtons()
        {
            SelectedStage
                .Throttle(TimeSpan.FromTicks(1))
                .Subscribe(st => startLevelButton.interactable = st != null);
            
            startLevelButton.onClick.AddListener(() =>
            {
                _stateMachine.Enter<LoadLevelState, LevelStaticData>(SelectedStage.Value);
            });

            // settingsButton.onClick.AddListener(() =>
            //     settingsWindow
            //         .InitAndShow(_persistentDataService.Settings)
            //         .Then(ok =>
            //         {
            //             settingsButton.OnPromisedResolve();
            //             if (ok) _saveLoadService.SaveSettings(); // TODO: else -> _sls.RestoreSavedSettings?
            //         })
            // );
        }
    }
}
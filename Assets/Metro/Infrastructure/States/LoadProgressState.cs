using Metro.Data;
using Metro.Infrastructure.States.Interfaces;
using Metro.Services.PersistentData;
using Metro.Services.SaveLoad;
using Metro.Services.StaticData;

namespace Metro.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(
            GameStateMachine gameStateMachine,
            IStaticDataService staticDataService,
            IPersistentDataService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = gameStateMachine;
            _staticDataService = staticDataService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _stateMachine.Enter<LoadMetaState>();
        }

        public void Exit()
        {
            
        }

        private async void LoadProgressOrInitNew()
        {
            _progressService.Settings = 
                await _saveLoadService.LoadSettings() 
                ?? NewSettings();
            
            _progressService.Progress = 
                await _saveLoadService.LoadProgress() 
                ?? NewProgress();
        }
        
        private PlayerProgressData NewProgress() =>
            new()
            {
                MaxCompletedLevel = 0
            };

        private PlayerSettingsData NewSettings() =>
            new()
            {
                MusicVolume = 100,
                SfxVolume = 100,
                DebugEnabled = false,
                HapticEnabled = true
            };
    }
}
﻿using UnityEngine;
using Metro.Infrastructure.Factories.Interfaces;
using Metro.Infrastructure.States.Interfaces;
using Metro.Services.Input;

namespace Metro.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ILevelFactory _levelFactory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IInputService _inputService;

        public GameLoopState(GameStateMachine gameStateMachine,
            IPlayerFactory playerFactory,
            ILevelFactory levelFactory,
            IUIFactory uiFactory,
            IInputService inputService)
        {
            _stateMachine = gameStateMachine;
            _levelFactory = levelFactory;
            _playerFactory = playerFactory;
            _uiFactory = uiFactory;
            _inputService = inputService;
        }

        public void Enter()
        {
            _levelFactory.Train.Run();
        }

        public void Exit()
        {
            // _playerFactory.PlayerController.Activate(false);
            
            _playerFactory.CleanUp();
            _levelFactory.CleanUp();
            //_uiFactory.HUDController.CleanUp();
        }

        private void SetupCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
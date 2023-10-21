using System;
using Metro.Services.Logging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using static UnityEngine.InputSystem.InputAction;
using Touch = UnityEngine.Touch;

namespace Metro.Services.Input
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private PlayerControls _controls;
        private readonly ILoggingService _logger;

        public Vector2 Move { get; private set; }
        public UnityAction<Vector2> Tap { get; set; }

        public InputService(ILoggingService logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            _controls = new PlayerControls();
            _controls.Enable();
            SubscribeOnControls(true);
            
            TouchSimulation.Enable();
        }

        public void Dispose()
        {
            SubscribeOnControls(false);
            _controls.Disable();
        }

        private void SubscribeOnControls(bool value)
        {
            if (value)
            {
                _controls.Player.Move.performed += OnMove;
                _controls.Player.Tap.performed += OnTap;
                _controls.Player.Move.canceled  += OnMove;
            }
            else
            {
                _controls.Player.Move.performed -= OnMove;
                _controls.Player.Tap.performed -= OnTap;
                _controls.Player.Move.canceled  -= OnMove;
            }
        }

        #region Adapter methods

        private void OnMove(CallbackContext ctx) => Move = ctx.ReadValue<Vector2>();
        private void OnTap(CallbackContext ctx) => Tap?.Invoke(ctx.ReadValue<Vector2>());

        #endregion
    }
}
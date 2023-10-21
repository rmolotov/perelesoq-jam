using Metro.Gameplay.Enemies;
using Metro.Services.Input;
using Metro.Services.Logging;
using Metro.StaticData.Player;
using UnityEngine;
using Zenject;

namespace Metro.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        private IInputService _inputService;
        private ILoggingService _logger;

        [SerializeField] private PlayerMove moveComponent;
        

        [Inject]
        private void Construct(IInputService inputService, ILoggingService logger)
        {
            _inputService = inputService;
            _logger = logger;
        }

        public void Initialize(PlayerStaticData config)
        {
            _inputService.Tap += HandleTap;
            
            moveComponent?.Initialize(config);
        }

        public void Run() => 
            moveComponent?.Run();

        public void Stop() =>
            moveComponent?.Stop();

        private void OnDestroy() => 
            _inputService.Tap -= HandleTap;

        

        private void HandleTap(Vector2 value)
        {
            // _logger.LogMessage(value.ToString(), this);
            
            var ray = Camera.main.ScreenPointToRay(value);

            if (Physics.Raycast(ray.origin, ray.direction, out var hit, 100))
                if (hit.collider != null && hit.collider.GetComponentInParent<EnemyMove>())
                    hit.collider.GetComponentInParent<EnemyMove>().Tap();
        }
    }
}
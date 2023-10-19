using System.Threading.Tasks;
using Metro.Infrastructure.States.Interfaces;
using Metro.Services.StaticData;

namespace Metro.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(
            GameStateMachine stateMachine, 
            IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }
        public async void Enter()
        {
            await Task.WhenAll(
                _staticDataService.Initialize()
            );
            
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
            
        }
    }
}
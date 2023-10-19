namespace Metro.Infrastructure.States.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
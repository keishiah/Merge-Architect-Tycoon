namespace _Scripts.Infrastructure.States
{
    public interface IExitableState
    {
        void SetGameStateMachine(IGameStateMachine gameStateMachine);
        void Exit();
    }
}
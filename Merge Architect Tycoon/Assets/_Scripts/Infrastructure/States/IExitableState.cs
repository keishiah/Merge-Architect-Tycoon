public interface IExitableState
{
    void SetGameStateMachine(IGameStateMachine gameStateMachine);
    void Exit();
}
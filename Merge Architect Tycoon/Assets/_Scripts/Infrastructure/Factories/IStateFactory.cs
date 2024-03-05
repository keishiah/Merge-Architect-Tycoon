using UnityEngine;

public interface IStateFactory
{
    T CreateState<T>(IGameStateMachine gameStateMachine) where T : IExitableState;
    GameObject CreateGameBootstrapper();
}
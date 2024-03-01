using _Scripts.Infrastructure.States;
using UnityEngine;

namespace _Scripts.Infrastructure.Factories
{
    public interface IStateFactory
    {
        T CreateState<T>(IGameStateMachine gameStateMachine) where T : IExitableState;
        GameObject CreateGameBootstrapper();
    }
}
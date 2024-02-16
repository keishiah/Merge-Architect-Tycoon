using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IStateFactory
    {
        T CreateState<T>(IGameStateMachine gameStateMachine) where T : IExitableState;
        GameObject CreateGameBootstrapper();
    }
}
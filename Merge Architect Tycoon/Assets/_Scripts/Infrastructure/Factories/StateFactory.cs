using _Scripts.Infrastructure.AssetManagment;
using _Scripts.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace _Scripts.Infrastructure.Factories
{
    public class StateFactory : IStateFactory
    {
        private DiContainer _container;

        [Inject]
        public StateFactory(DiContainer container)
        {
            _container = container;
        }

        public T CreateState<T>(IGameStateMachine gameStateMachine) where T : IExitableState
        {
            var state = _container.Resolve<T>();
            state.SetGameStateMachine(gameStateMachine);
            return state;
        }

        public GameObject CreateGameBootstrapper()
        {
            return _container.InstantiatePrefabResource(AssetPath.GameBootsTrapper);
        }
    }
}
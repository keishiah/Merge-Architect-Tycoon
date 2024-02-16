using CodeBase.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        IStateFactory _stateFactory;

        [Inject]
        void Construct(IStateFactory stateFactory) =>
            _stateFactory = stateFactory;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null) return;

            _stateFactory.CreateGameBootstrapper();
        }
    }
}
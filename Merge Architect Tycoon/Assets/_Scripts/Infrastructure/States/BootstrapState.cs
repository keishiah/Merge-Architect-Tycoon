using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void SetGameStateMachine(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await InitServices();
            _gameStateMachine.Enter<LoadPlayerProgressState>();
            
        }

        private async UniTask InitServices()
        {
            await _staticDataService.Initialize();
        }

        public void Exit()
        {
        }
    }
}
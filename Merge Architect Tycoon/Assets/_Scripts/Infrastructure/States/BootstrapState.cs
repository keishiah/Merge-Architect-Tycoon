using Cysharp.Threading.Tasks;

public class BootstrapState : IState
{
    private IGameStateMachine _gameStateMachine;
    private readonly StaticDataService _staticDataService;
    private FirebaseLogger _firebaseLogger;

    public BootstrapState(StaticDataService staticDataService, FirebaseLogger firebaseLogger)
    {
        _staticDataService = staticDataService;
        _firebaseLogger = firebaseLogger;
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
        await _firebaseLogger.InitializeFirebase();
    }

    public void Exit()
    {
    }
}
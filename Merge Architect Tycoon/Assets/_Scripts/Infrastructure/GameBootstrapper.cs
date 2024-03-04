using UnityEngine;
using Zenject;

public class GameBootstrapper : MonoBehaviour
{
    private IGameStateMachine _gameStateMachine;

    [Inject]
    void Construct(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void Start()
    {
        _gameStateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}
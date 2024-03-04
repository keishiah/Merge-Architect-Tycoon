using UnityEngine;
using Zenject;

public class GameRunner : MonoBehaviour
{
    private IStateFactory _stateFactory;

    [Inject]
    void Construct(IStateFactory stateFactory)
    {
        _stateFactory = stateFactory;
    }

    private void Start()
    {
        CreateGameBootstrapper();
    }

    private void CreateGameBootstrapper()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper != null) return;

        _stateFactory.CreateGameBootstrapper();
    }
}
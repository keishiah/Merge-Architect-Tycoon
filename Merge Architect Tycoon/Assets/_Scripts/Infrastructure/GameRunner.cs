using System.Text.RegularExpressions;
using UnityEngine;
using Zenject;

public class GameRunner : MonoBehaviour
{
    private const string gameVersionKey = "Version";
    private IStateFactory _stateFactory;
    [SerializeField] private bool _skipTutorial;

    [Inject]
    void Construct(IStateFactory stateFactory)
    {
        _stateFactory = stateFactory;
    }

    private void Start()
    {
        CheckTutorial();
        CheckVersion();
        CreateGameBootstrapper();
    }

    private void CheckTutorial()
    {
        if (!_skipTutorial)
            PlayerPrefs.SetString(SaveKey.NeedSkipTutorial.ToString(), "false");
        else
            PlayerPrefs.SetString(SaveKey.NeedSkipTutorial.ToString(), "true");
    }

    //If new version => delete all saves
    //Regex: https://regex101.com/
    private void CheckVersion()
    {
        string numbersOnlyVersion = Regex.Replace(Application.version, "[^0-9.]", "");

        if (!PlayerPrefs.HasKey(gameVersionKey)
            || numbersOnlyVersion != PlayerPrefs.GetString(gameVersionKey))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString(gameVersionKey, numbersOnlyVersion);
        }
    }

    private void CreateGameBootstrapper()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper != null) return;

        _stateFactory.CreateGameBootstrapper();
    }
}
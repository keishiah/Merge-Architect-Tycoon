using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using Zenject;

public class GameRunner : MonoBehaviour
{
    private bool _isNeedDeleteAll = false;
    private IStateFactory _stateFactory;
    [SerializeField] private bool _skipTutorial;
    [SerializeField] private bool _clearPrefs;

    [Inject]
    void Construct(IStateFactory stateFactory)
    {
        _stateFactory = stateFactory;
    }

    private IEnumerator Start()
    {
        CheckSaves();
        //if need delete all prefs, await new frame.
        if(!_isNeedDeleteAll)
            yield return null;
        CheckTutorial();
        CheckVersion();
        CreateGameBootstrapper();
    }

    //Regex: https://regex101.com/
    //If new version => delete all saves
    private void CheckSaves()
    {
        string numbersOnlyVersion = Regex.Replace(Application.version, "[^0-9.]", "");

        TutorialData Tutorial = SaveLoadService.Load<TutorialData>(SaveKey.Tutorial);

        if (!PlayerPrefs.HasKey(SaveKey.GameVersion.ToString())
            || numbersOnlyVersion != PlayerPrefs.GetString(SaveKey.GameVersion.ToString())
            || _clearPrefs
            || (Tutorial != null && !Tutorial.IsComplite && Tutorial.StepIndex > 0))
        {
            _isNeedDeleteAll = true;
            PlayerPrefs.DeleteAll();
            Debug.Log("DeleteAll2");
        }
    }

    private void CheckTutorial()
    {
        if (_skipTutorial)
            PlayerPrefs.SetString(SaveKey.NeedSkipTutorial.ToString(), "true");
        else
            PlayerPrefs.SetString(SaveKey.NeedSkipTutorial.ToString(), "false");
    }

    private void CheckVersion()
    {
        string numbersOnlyVersion = Regex.Replace(Application.version, "[^0-9.]", "");

        if (_isNeedDeleteAll)
        {
            PlayerPrefs.SetString(SaveKey.GameVersion.ToString(), numbersOnlyVersion);
        }
    }

    private void CreateGameBootstrapper()
    {
        var bootstrapper = FindObjectOfType<GameBootstrapper>();

        if (bootstrapper != null) return;

        _stateFactory.CreateGameBootstrapper();
    }
}
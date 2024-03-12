using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class PreStart : MonoBehaviour
{
    [Inject] private IPlayerProgressService _playerProgress;
    [SerializeField] private GameObject _playButton;

    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private GameObject _gameRunner;
    [SerializeField] private Toggle _tutorialToggle;

    private float fakeLoadDuration = .5f;

    private IEnumerator Start()
    {
        yield return null;

        IsNeedTutorial();
        _gameRunner.SetActive(true);
        DontDestroyOnLoad(this);
    }

    public void FakeLoad()
    {
        _playerProgress.Progress.Tutorial.IDontNeedTutorial = !_tutorialToggle.isOn;
        StartCoroutine(fakeLoad());
    }

    private void IsNeedTutorial()
    {
        TutorialData tutorialData;

        _playerProgress.Progress.Tutorial =
            tutorialData =
            SaveLoadService.Load<TutorialData>(SaveKey.Tutorial);

        if (!tutorialData.IsComplite 
            && tutorialData.StepIndex <= 0)
        {
            _tutorialToggle.gameObject.SetActive(true);
            _tutorialToggle.isOn = !tutorialData.IDontNeedTutorial;
        }
    }

    private IEnumerator fakeLoad()
    {
        _playButton.SetActive(false);
        _loadingSlider.gameObject.SetActive(true);
        _loadingSlider.value = 0;

        float step = 1 / fakeLoadDuration;

        while (_loadingSlider.value < 0.8f)
        {
            yield return null;
            _loadingSlider.value += step * Time.deltaTime;
        }

        while (SceneManager.GetActiveScene().name != AssetPath.StartGameScene)
            yield return null;

        while (_loadingSlider.value < 1f)
        {
            yield return null;
            _loadingSlider.value += step * Time.deltaTime;
        }

        // Destroy(gameObject);
        SceneManager.UnloadSceneAsync(0);
    }
}
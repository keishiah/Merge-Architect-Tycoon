using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreStart : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;

    [SerializeField] private Slider _loadingSlider;
    [SerializeField] private GameObject _gameRunner;

    private float fakeLoadDuration = .5f;

    private IEnumerator Start()
    {
        yield return null;

        _gameRunner.SetActive(true);
        DontDestroyOnLoad(this);
    }

    public void FakeLoad()
    {
        StartCoroutine(fakeLoad());
    }

    private IEnumerator fakeLoad()
    {
        _playButton.SetActive(false);
        _loadingSlider.gameObject.SetActive(true);
        _loadingSlider.value = 0;

        yield return null;

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
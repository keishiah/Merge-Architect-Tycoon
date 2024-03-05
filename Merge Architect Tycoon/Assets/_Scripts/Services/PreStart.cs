using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreStart : MonoBehaviour
{
    [SerializeField] private GameObject Button;

    [SerializeField] private Slider LoadingSlider;
    [SerializeField] private GameObject GameRunner;

    private float fakeLoadDuration = .5f;

    private IEnumerator Start()
    {
        yield return null;
        GameRunner.SetActive(true);
        DontDestroyOnLoad(this);
    }

    public void FakeLoad()
    {
        StartCoroutine(fakeLoad());
    }

    private IEnumerator fakeLoad()
    {
        Button.SetActive(false);
        LoadingSlider.gameObject.SetActive(true);
        LoadingSlider.value = 0;

        float step = 1 / fakeLoadDuration;

        while (LoadingSlider.value < 0.8f)
        {
            yield return null;
            LoadingSlider.value += step * Time.deltaTime;
        }

        while (SceneManager.GetActiveScene().name != AssetPath.StartGameScene)
            yield return null;

        while (LoadingSlider.value < 1f)
        {
            yield return null;
            LoadingSlider.value += step * Time.deltaTime;
        }

        // Destroy(gameObject);
        SceneManager.UnloadSceneAsync(0);
    }
}
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
    [SerializeField] private GameObject _dialogTextHolder;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _allScreenButton;

    [SerializeField] private GameObject _blockerator;
    [SerializeField] private GameObject _sceneButtonsBlocker;
    [SerializeField] private Button _buttonToClick;

    [SerializeField] private GameObject _handPointer;
    [SerializeField] private Animation _handAnimation;

    public List<TutorialStep> TutorialSteps = new();

    private int _currentStep = 0;
    private Button _tempButton;

    private void Awake()
    {

        DisableAll();
    }

    public void StartTutorialFromIndex(int stepIndex)
    {
        _currentStep = stepIndex;
        TutorialSteps[_currentStep].Enter(this);
    }
    private void DisableAll()
    {
        _dialog.SetActive(false);
        _handPointer.SetActive(false);
        _sceneButtonsBlocker.SetActive(false);
        _blockerator.SetActive(false);
        _blockerator.GetComponent<Image>().enabled = true;

    }

    private void NextStep()
    {
        DisableAll();

        _currentStep++;
        TutorialData tutorialData = new TutorialData();
        tutorialData.StepIndex = _currentStep;


        if (_currentStep >= TutorialSteps.Count)
        {
            tutorialData.IsComplite = true;
            Destroy(gameObject);
        }
        else
            TutorialSteps[_currentStep].Enter(this);

        SaveLoadService.Save(SaveKey.Tutorial, tutorialData);
    }

    public void ShowDialog(string text)
    {
        _dialog.SetActive(true);
        _dialogText.text = text;
    }

    public void NextButtonReset(Button buttonToNext = null)
    {
        if (buttonToNext != null)
        {
            CreateButtonImage(buttonToNext);
        }
        else
        {
            _blockerator.SetActive(false);
            _tempButton = null;
            _allScreenButton.SetActive(true);
        }
    }

    private void CreateButtonImage(Button buttonToNext)
    {
        _blockerator.SetActive(true);
        CreateTempButton(buttonToNext);


        _tempButton = buttonToNext;
        _allScreenButton.SetActive(false);
    }
    
    public async UniTask NextMovingButtonReset(Button buttonNext)
    {
        if (buttonNext != null)
        {
            buttonNext.gameObject.SetActive(false);
            _blockerator.SetActive(true);
            _blockerator.GetComponent<Image>().enabled = false;
            _sceneButtonsBlocker.SetActive(true);

            await CheckButtonMovement(buttonNext);

            CreateTempButton(buttonNext);
        }
        else
        {
            _blockerator.SetActive(false);
            _tempButton = null;
            _allScreenButton.SetActive(true);
        }
    }

    private void CreateTempButton(Button buttonNext)
    {
        _buttonToClick.GetComponent<Image>().sprite = buttonNext.GetComponent<Image>().sprite;
        RectTransform buttonToClickRectTransform = _buttonToClick.GetComponent<RectTransform>();
        RectTransform buttonToNextRect = buttonNext.GetComponent<RectTransform>();

        buttonToClickRectTransform.pivot = buttonToNextRect.pivot;
        buttonToClickRectTransform.position = buttonToNextRect.position;
        buttonToClickRectTransform.sizeDelta = buttonToNextRect.sizeDelta;
        _tempButton = buttonNext;
    }


    private async UniTask CheckButtonMovement(Button button)
    {
        Vector3 previousPosition = button.transform.position;

        while (true)
        {
            await UniTask.DelayFrame(100);

            if (button.transform.position != previousPosition)
            {
                previousPosition = button.transform.position;
            }
            else
            {
                break;
            }
        }
    }

    public void ClickOnButton()
    {
        if (_tempButton == null)
        {
            Debug.LogError("_tempButton is null!!!");
            return;
        }

        _tempButton.onClick.Invoke();

        NextStep();
    }

    public void ShowHand(AnimationClip clip, Transform transform = null)
    {
        _handPointer.SetActive(true);
        _handAnimation.clip = clip;
        _handAnimation.Play();
    }
}
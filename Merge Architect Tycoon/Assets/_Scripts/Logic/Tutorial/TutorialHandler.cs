using Cysharp.Threading.Tasks;
using System;
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
    [SerializeField] private Button _buttonToClick;

    [SerializeField] private GameObject _handPointer;
    [SerializeField] private Transform _handParent;
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
        _buttonToClick.enabled = false;
        _blockerator.SetActive(false);
        _blockerator.GetComponent<Image>().enabled = true;

    }

    public void NextStep()
    {
        DisableAll();

        _currentStep++;
        TutorialData tutorialData = new TutorialData();
        tutorialData.StepIndex = _currentStep;


        if (_currentStep >= TutorialSteps.Count)
        {
            tutorialData.IsComplite = true;
            Destroy(gameObject);
            SaveLoadService.Save(SaveKey.Tutorial, tutorialData);
            return;
        }

        TutorialSteps[_currentStep].Enter(this);

        if(TutorialSteps[_currentStep].IsKeyStep)
            SaveLoadService.Save(SaveKey.Tutorial, tutorialData);
    }

    public void ShowDialog(string text)
    {
        _dialogText.text = text;
        _dialog.SetActive(true);
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
            _blockerator.SetActive(true);

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
        _buttonToClick.enabled = true;
        RectTransform buttonToClickRectTransform = _buttonToClick.GetComponent<RectTransform>();
        RectTransform buttonToNextRect = buttonNext.GetComponent<RectTransform>();

        buttonToClickRectTransform.pivot = buttonToNextRect.pivot;
        buttonToClickRectTransform.position = buttonToNextRect.position;
        buttonToClickRectTransform.sizeDelta = buttonToNextRect.rect.size;

        _tempButton = buttonNext;
    }
    public void BadCodeCreateTempButton(Button buttonNext)
    {
        _blockerator.SetActive(true);
        _buttonToClick.enabled = true;
        RectTransform buttonToClickRectTransform = _buttonToClick.GetComponent<RectTransform>();
        RectTransform buttonToNextRect = buttonNext.GetComponent<RectTransform>();

        buttonToClickRectTransform.pivot = buttonToNextRect.pivot;
        buttonToClickRectTransform.position = new(212*.81f,258*.81f,0);
        buttonToClickRectTransform.sizeDelta = buttonToNextRect.rect.size;

        _tempButton = buttonNext;
        _allScreenButton.SetActive(false);
    }

    private async UniTask CheckButtonMovement(Button button)
    {
        Vector3 previousPosition = button.transform.position;

        while (true)
        {
            await UniTask.DelayFrame(10);

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
        if(transform != null)
            _handPointer.transform.SetParent(transform, false);
        else
            _handPointer.transform.SetParent(_handParent, false);

        _handPointer.SetActive(true);
        _handAnimation.clip = clip;
        _handAnimation.Play();
    }
}
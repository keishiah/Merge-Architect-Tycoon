using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject _dialog;
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
        TutorialSteps[_currentStep].Enter(this);
    }

    private void DisableAll()
    {
        _dialog.SetActive(false);
        _handPointer.SetActive(false);
        //_handPointer.transform.SetParent(this.transform);
        _blockerator.SetActive(false);
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

        //SaveLoadService.Save(SaveKey.Tutorial, tutorialData);
    }

    public void ShowDialog(string text)
    {
        _dialog.SetActive(true);
        _dialogText.text = text;
    }

    public void NextButtonReset(Action<Button> tutorialAction,Button buttonToNext = null)
    {
        if (buttonToNext != null)
        {
            tutorialAction?.Invoke(buttonToNext);
        }
        else
        {
            _blockerator.SetActive(false);
            _tempButton = null;
            _allScreenButton.SetActive(true);
        }
    }

    public void CreateButtonImage(Button buttonToNext)
    {
        _blockerator.SetActive(true);
        _buttonToClick.GetComponent<Image>().sprite = buttonToNext.GetComponent<Image>().sprite;

        RectTransform buttonToClickRectTransform = _buttonToClick.GetComponent<RectTransform>();
        RectTransform buttonToNextRect = buttonToNext.GetComponent<RectTransform>();

        buttonToClickRectTransform.pivot = buttonToNextRect.pivot;
        buttonToClickRectTransform.position = buttonToNextRect.position;
        buttonToClickRectTransform.sizeDelta = buttonToNextRect.sizeDelta;

        _tempButton = buttonToNext;
        _allScreenButton.SetActive(false);
    }

    public void CreateMovingButtonBlocker(Button buttonNext)
    {
        CreateButtonImage(buttonNext);
        _blockerator.GetComponent<Image>().enabled = false;
        _sceneButtonsBlocker.SetActive(true);
        
        buttonNext.OnClickAsObservable()
            .Subscribe(_ => OnMovingButtonClicked())
            .AddTo(this); 
    }

    private void OnMovingButtonClicked()
    {
        
        _blockerator.GetComponent<Image>().enabled = true;
        _sceneButtonsBlocker.SetActive(false);
        NextStep();
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

        //if (transform != null)
        //    _handPointer.transform.SetParent(transform);
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialog;
    [SerializeField]
    private TextMeshProUGUI _dialogText;
    [SerializeField]
    private GameObject _allScreenButton;

    [SerializeField]
    private GameObject _blockerators;
    [SerializeField]
    private GameObject _upBlock;
    [SerializeField]
    private GameObject _downBlock;
    [SerializeField]
    private GameObject _leftBlock;
    [SerializeField]
    private GameObject _rightBlock;

    [SerializeField]
    private GameObject _handPointer;
    [SerializeField]
    private Animation _handAnimation;

    public List<TutorialStep> TutorialSteps = new();

    private int _currentStep = 0;
    private Button _tempButton;

    private void Awake()
    {
        DisableAll();
    }

    private void DisableAll()
    {
        _dialog.SetActive(false);
        _handPointer.SetActive(false);
        _blockerators.SetActive(false);
    }

    public void NextStep()
    {
        DisableAll();

        _currentStep++;
        TutorialData tutorialData = new TutorialData();
        tutorialData.StepIndex = _currentStep;

        TutorialSteps[_currentStep].Enter(this);

        if (_currentStep >= TutorialSteps.Count)
        {
            tutorialData.IsComplite = true;
            Destroy(gameObject);
        }

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
            _tempButton = buttonToNext;
            _allScreenButton.SetActive(false);
            //invoke only onse!
            Debug.Log(buttonToNext.name + ": AddListener!");
            buttonToNext.onClick.AddListener(ClearListener);
            buttonToNext.onClick.AddListener(NextStep);
        }
        else
        {
            _tempButton = null;
            _allScreenButton.SetActive(true);
        }
    }

    private void ClearListener()
    {
        Debug.Log(_tempButton.name + ": RemoveListener!");
        _tempButton?.onClick.RemoveListener(NextStep);
        _tempButton?.onClick.RemoveListener(ClearListener);
    }

    public void ShowHand(AnimationClip clip)
    {
        _handPointer.SetActive(true);
        _handAnimation.clip = clip;
    }
}

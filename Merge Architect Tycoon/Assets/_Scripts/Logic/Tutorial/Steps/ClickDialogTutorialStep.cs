using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ClickDialogTutorialStep", menuName = "StaticData/Tutorial/ClickDialogTutorialStep")]
public class ClickDialogTutorialStep : TutorialStep
{
    public AnimationClip clip;
    public string buttonName;
    [Multiline]
    public string text;

    public override void Enter(TutorialHandler tutorialHandler)
    {
        //fail fast principle!
        GameObject buttonGameObject = GameObject.Find(buttonName);
        Button button = buttonGameObject.GetComponent<Button>();

        tutorialHandler.NextButtonReset(tutorialHandler.CreateButtonImage,button);
        tutorialHandler.ShowDialog(text);
        tutorialHandler.ShowHand(clip, buttonGameObject.transform);
    }
}
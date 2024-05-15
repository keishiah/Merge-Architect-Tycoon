using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ClickWhenButtonStopTutorialStep",
    menuName = "StaticData/Tutorial/ClickWhenButtonStopTutorialStep")]
public class ClickWhenButtonStopTutorialStep : TutorialStep
{
    public AnimationClip clip;
    public string buttonName;

    public override async void Enter(TutorialHandler tutorialHandler)
    {
        //fail fast principle!
        GameObject buttonGameObject = GameObject.Find(buttonName);
        Button button = buttonGameObject.GetComponent<Button>();

        await tutorialHandler.NextMovingButtonReset(button);

        tutorialHandler.NextButtonReset(button);
        tutorialHandler.ShowHand(clip);
    }
}

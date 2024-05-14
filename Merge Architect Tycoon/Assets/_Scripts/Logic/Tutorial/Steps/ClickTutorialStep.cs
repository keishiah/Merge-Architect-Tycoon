using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ClickTutorialStep", menuName = "StaticData/Tutorial/ClickTutorialStep")]
public class ClickTutorialStep : TutorialStep
{
    public AnimationClip clip;
    public string buttonName;

    public override void Enter(TutorialHandler tutorialHandler)
    {
        //fail fast principle!
        GameObject buttonGameObject = GameObject.Find(buttonName);
        Button button = buttonGameObject.GetComponent<Button>();

        tutorialHandler.NextButtonReset(button);
        tutorialHandler.ShowHand(clip);
    }
}
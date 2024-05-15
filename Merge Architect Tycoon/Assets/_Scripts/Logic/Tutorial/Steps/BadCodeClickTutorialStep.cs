using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BadCodeClickTutorialStep", menuName = "StaticData/Tutorial/BadCodeClickTutorialStep")]
public class BadCodeClickTutorialStep : TutorialStep
{
    public AnimationClip clip;
    public string buttonName;

    public override void Enter(TutorialHandler tutorialHandler)
    {
        //fail fast principle!
        GameObject buttonGameObject = GameObject.Find(buttonName);
        Button button = buttonGameObject.GetComponent<Button>();

        tutorialHandler.BadCodeCreateTempButton(button);
        tutorialHandler.ShowHand(clip);
    }
}
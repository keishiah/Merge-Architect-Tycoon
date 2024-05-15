using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MergeTutorialStep",
    menuName = "StaticData/Tutorial/MergeTutorialStep")]
public class MergeTutorialStep : TutorialStep
{
    public AnimationClip clip;
    public string buttonName;

    private TutorialHandler tutorialHandler;

    public override void Enter(TutorialHandler tutorialHandler)
    {
        this.tutorialHandler = tutorialHandler;

        //fail fast principle!
        GameObject buttonGameObject = GameObject.Find(buttonName);
        Button button = buttonGameObject.GetComponent<Button>();

        SlotRenderer.MergeEvent += NextStep;

        tutorialHandler.ShowHand(clip, buttonGameObject.transform);
    }

    private void NextStep()
    {
        SlotRenderer.MergeEvent -= NextStep;
        tutorialHandler.NextStep();
    }
}
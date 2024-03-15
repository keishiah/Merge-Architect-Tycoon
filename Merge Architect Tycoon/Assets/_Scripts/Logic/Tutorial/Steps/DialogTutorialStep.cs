using UnityEngine;

[CreateAssetMenu(fileName = "DialogTutorialStep", menuName = "StaticData/Tutorial/DialogTutorialStep")]
public class DialogTutorialStep : TutorialStep
{
    [Multiline]
    public string text;

    public override void Enter(TutorialHandler tutorialHandler)
    {
        tutorialHandler.ShowDialog(text);
        tutorialHandler.NextButtonReset();
    }
}

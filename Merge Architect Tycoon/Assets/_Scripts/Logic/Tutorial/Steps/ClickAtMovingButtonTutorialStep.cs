using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Logic.Tutorial.Steps
{
    [CreateAssetMenu(fileName = "ClickAtMovingButtonTutorialStep",
        menuName = "StaticData/Tutorial/ClickAtMovingButtonTutorialStep")]
    public class ClickAtMovingButtonTutorialStep : TutorialStep
    {
        public AnimationClip clip;
        public string buttonName;

        public override void Enter(TutorialHandler tutorialHandler)
        {
            //fail fast principle!
            GameObject buttonGameObject = GameObject.Find(buttonName);
            Button button = buttonGameObject.GetComponent<Button>();
            tutorialHandler.NextButtonReset(tutorialHandler.CreateMovingButtonBlocker, button);
            tutorialHandler.ShowHand(clip, buttonGameObject.transform);
        }

    }
}
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Logic.Tutorial.Steps
{
    [CreateAssetMenu(fileName = "ClickWithDownBlockTutorialStep",
        menuName = "StaticData/Tutorial/ClickWithDownBlockTutorialStep")]
    public class ClickWithDownBlockTutorialStep : TutorialStep
    {
        public AnimationClip clip;
        public string buttonName;

        public override async void Enter(TutorialHandler tutorialHandler)
        {
            GameObject buttonGameObject = GameObject.Find(buttonName);
            Button button = buttonGameObject.GetComponent<Button>();
            await tutorialHandler.NextMovingButtonReset(button);
            tutorialHandler.ShowHand(clip, buttonGameObject.transform);
        }
    }
}
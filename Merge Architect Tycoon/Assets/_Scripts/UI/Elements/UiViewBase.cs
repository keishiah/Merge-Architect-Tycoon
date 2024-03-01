using UnityEngine;

namespace _Scripts.UI.Elements
{
    public abstract class UiViewBase : MonoBehaviour
    {
        public abstract void InitUiElement(UiPresenter uiPresenter);
    }
}
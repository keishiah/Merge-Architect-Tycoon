using UnityEngine;

namespace CodeBase.UI.Elements
{
    public abstract class UiViewBase : MonoBehaviour
    {
        public abstract void InitUiElement(UiPresenter uiPresenter);
    }
}
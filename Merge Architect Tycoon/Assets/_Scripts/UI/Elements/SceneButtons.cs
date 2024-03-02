using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class SceneButtons : MonoBehaviour
    {
        private int _selectedButtonIndex = -1;

        [SerializeField]
        private Button[] _menuButtons;

        [SerializeField]
        private GameObject[] _panel;

        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;

        [Inject]
        void Construct(CreateBuildingPopupPresenter createBuildingPopupPresenter)
        {
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
        }

        private void Awake()
        {
            for(int i = 0; i < _menuButtons.Length; i++)
            {
                int index = i;//allocate new "instance" EACH Step of loop
                _menuButtons[i].onClick.AddListener(() => { OnMenuButtonClick(index); });
            }
        }
        public void OnMenuButtonClick(int i)
        {
            bool needToSelect = _selectedButtonIndex != i;

            if (_selectedButtonIndex != -1)
            {
                _panel[_selectedButtonIndex].SetActive(false);
                _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(TriggerNormal);
                _selectedButtonIndex = -1;
            }

            if (!needToSelect)
                return;

            _selectedButtonIndex = i;
            _panel[_selectedButtonIndex].SetActive(true);
            _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(TriggerSelected);
        }

        private const string TriggerNormal = "Normal";
        private const string TriggerSelected = "Selected";
    }
}
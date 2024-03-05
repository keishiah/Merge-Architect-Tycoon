using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum SceneButtonsEnum
{
    Quest, Merge, Build, District, Shop
}
public class SceneButtons : MonoBehaviour
{
    private int _selectedButtonIndex = -1;

    [SerializeField]
    private Button[] _menuButtons;
    [SerializeField]
    private Widget[] _widgets;

    private const string AnimatorTriggerNormal = "Normal";
    private const string AnimatorTriggerSelected = "Selected";

    private void Awake()
    {
        for(int i = 0; i < _menuButtons.Length; i++)
        {
            int index = i;//allocate new "instance" EACH Step of loop
            _menuButtons[i].onClick.AddListener(() => { OnMenuButtonClick(index); });
        }
    }
    public void OnMenuButtonClick(SceneButtonsEnum i)
    {
        OnMenuButtonClick((int)i);
    }
    public void CloseCurrentWidget()
    {
        if (_selectedButtonIndex == -1)
            return;

        _widgets[_selectedButtonIndex].OnDisable();
        _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(AnimatorTriggerNormal);
        _selectedButtonIndex = -1;
    }
    public void OnMenuButtonClick(int i)
    {
        bool needToSelect = _selectedButtonIndex != i;

        CloseCurrentWidget();

        if (!needToSelect)
            return;
        _selectedButtonIndex = i;
        _widgets[_selectedButtonIndex].OnEnable();
        _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(AnimatorTriggerSelected);
    }
}
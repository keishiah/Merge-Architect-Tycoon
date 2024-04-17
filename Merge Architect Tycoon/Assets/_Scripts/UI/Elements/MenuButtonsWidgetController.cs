using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum MenuButtonsEnum
{
    Quest,
    Merge,
    Build,
    District,
    Shop
}

public class MenuButtonsWidgetController : MonoBehaviour
{
    private int _selectedButtonIndex = -1;

    [SerializeField] private Button[] _menuButtons;
    [SerializeField] private Button[] _closeButtons;
    [SerializeField] private WidgetView[] _widgets;
    [Inject] private AudioPlayer _audioPlayer;
    [Inject] private CameraZoomer _cameraZoomer;
    [Inject] private BackGroundButton _backGroundButton;

    private const string AnimatorTriggerNormal = "Normal";
    private const string AnimatorTriggerSelected = "Selected";

    private void Awake()
    {
        for (int i = 0; i < _menuButtons.Length; i++)
        {
            int index = i; //allocate new "instance" EACH Step of loop
            _menuButtons[i].onClick.AddListener(() => { OnMenuButtonClick(index); });
        }

        for (int i = 0; i < _closeButtons.Length; i++)
        {
            _closeButtons[i].onClick.AddListener(() => { _audioPlayer.PlayUiSound(UiSoundTypes.MenuButtonClick); });
        }

        _backGroundButton.button.onClick.AddListener(CloseCurrentWidget);
    }

    public void OnMenuButtonClick(MenuButtonsEnum i)
    {
        OnMenuButtonClick((int)i);
    }

    public void CloseCurrentWidget()
    {
        for (int i = 0; i < _widgets.Length; i++)
        {
            _widgets[i].OnClose();
        }

        if (_selectedButtonIndex != -1)
            _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(AnimatorTriggerNormal);
        _selectedButtonIndex = -1;
    }

    private void OnMenuButtonClick(int i)
    {
        _audioPlayer.PlayUiSound(UiSoundTypes.MenuButtonClick);
        _cameraZoomer.MoveCameraBack();
        bool needToSelect = _selectedButtonIndex != i;

        CloseCurrentWidget();

        if (!needToSelect)
            return;
        _selectedButtonIndex = i;
        _widgets[_selectedButtonIndex].OnOpen();
        _menuButtons[_selectedButtonIndex].GetComponent<Animator>().SetTrigger(AnimatorTriggerSelected);
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelpButton : MonoBehaviour
{
    [Multiline]
    [SerializeField] private string HelpText;

    [SerializeField] private GameObject helpPanel;
    [SerializeField] private TextMeshProUGUI helpText;

    public void ShowHelp()
    {
        helpText.text = HelpText;
        helpPanel.SetActive(true);
    }
}

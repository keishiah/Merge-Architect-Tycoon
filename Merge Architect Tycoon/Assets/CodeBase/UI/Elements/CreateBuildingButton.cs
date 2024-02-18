using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CreateBuildingButton : MonoBehaviour
    {
        public Button createButton;
        public TextMeshProUGUI priceTex;

        public void SetButtonListener(UnityAction onLick)
        {
            createButton.onClick.AddListener(onLick);
        }

        public void SetPriceText(string text) => priceTex.text = text;

        public void SetCreateButtonInteractable(bool interactable) => createButton.interactable = interactable;

        private void OnDisable() => createButton.onClick.RemoveAllListeners();
    }
}
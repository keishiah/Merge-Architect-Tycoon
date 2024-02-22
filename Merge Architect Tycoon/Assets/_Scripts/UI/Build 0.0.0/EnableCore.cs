using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Build_0._0._0
{
    public class EnableCore : MonoBehaviour
    {
        public Button enableButton;
        public GameObject core;

        private void Start()
        {
            enableButton.onClick.AddListener(ActivateCore);
        }

        private void ActivateCore()
        {
            core.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
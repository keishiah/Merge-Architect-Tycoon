using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Elements
{
    public class CitiesMapPopup : MonoBehaviour
    {
        public Button openDistrictOne;
        public GameObject districtOne;

        public List<DistrictUi> districts;
        public string currentDistrict;

        private void Start()
        {
            openDistrictOne.onClick.AddListener(OpenDistrictOne);
        }

        private void OpenDistrictOne()
        {
            currentDistrict = districtOne.name;
            districtOne.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
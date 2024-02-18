using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.StaticDataService;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class CreateBuildingPopupPresenter
    {
        private ItemsCatalogue _itemsCatalogue;
        private IStaticDataService _staticDataService;

        [Inject]
        void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Construct(ItemsCatalogue itemsCatalogue)
        {
            _itemsCatalogue = itemsCatalogue;
        }

        public void SetUpBuildingButtons(List<CreateBuildingButton> buttons)
        {
            for (int x = 0; x < buttons.Count; x++)
            {
                if (x < _staticDataService.BuildingData.Values.ToList().Count)
                {
                    BuildingInfo buildingInfo = _staticDataService.BuildingData.Values.ToList()[x];
                    
                    buttons[x].priceTex.text =
                        buildingInfo.coinsCountToCreate.ToString();
                    buttons[x].createButton.interactable =
                        _itemsCatalogue.CheckHasItem(
                            buildingInfo.itemToCreate);
                    buttons[x].createButton.onClick.AddListener(() =>
                        TakeItem(buildingInfo.itemToCreate));
                }
            }
        }

        public void TakeItem(MergeItem item)
        {
            _itemsCatalogue.TakeItems(item, 1);
        }

        public void OpenCreateBuildingPopup()
        {
        }
    }
}
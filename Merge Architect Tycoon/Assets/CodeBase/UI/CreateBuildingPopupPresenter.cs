using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
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
        private BuildingProvider _buildingProvider;

        [Inject]
        void Construct(IStaticDataService staticDataService, ItemsCatalogue itemsCatalogue,
            BuildingProvider buildingProvider)
        {
            _staticDataService = staticDataService;
            _itemsCatalogue = itemsCatalogue;
            _buildingProvider = buildingProvider;
        }

        public void SetUpBuildingButtons(List<CreateBuildingButton> buttons)
        {
            for (int x = 0; x < buttons.Count; x++)
            {
                if (x < _staticDataService.BuildingData.Values.ToList().Count)
                {
                    BuildingInfo buildingInfo = _staticDataService.BuildingData.Values.ToList()[x];

                    buttons[x].SetPriceText(buildingInfo.coinsCountToCreate.ToString());

                    buttons[x].SetCreateButtonInteractable(_itemsCatalogue.CheckHasItem(
                        buildingInfo.itemToCreate));

                    buttons[x].SetButtonListener(() =>
                        CreateBuilding(buildingInfo.itemToCreate, buildingInfo.buildingName));
                }
            }
        }

        private void CreateBuilding(MergeItem item, string buildingName)
        {
            _itemsCatalogue.TakeItems(item, 1);
            _buildingProvider.CreateBuilding(buildingName);
        }
    }
}
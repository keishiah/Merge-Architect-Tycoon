using CodeBase.Infrastructure.States;
using CodeBase.Services;
using CodeBase.UI.Elements;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class QuestsPresenter
    {
        private IPlayerProgressService _playerProgressService;

        private QuestWidget _questWidget;
        private CreateBuildingPopupPresenter _createBuildingPopupPresenter;
        private LoadLevelState _loadLevelState;

        [Inject]
        void Construct(IPlayerProgressService playerProgressService,
            CreateBuildingPopupPresenter createBuildingPopupPresenter, LoadLevelState loadLevelState)
        {
            _playerProgressService = playerProgressService;
            _createBuildingPopupPresenter = createBuildingPopupPresenter;
            _loadLevelState = loadLevelState;
        }

        public void InitializeWidget()
        {
            _playerProgressService.Progress.Coins.SubscribeToCoinsCountChanges(SetWidgetValues);
        }


        public void SetQuestWidget(QuestWidget questWidget)
        {
            _questWidget = questWidget;
        }

        private void SetWidgetValues(int coins)
        {
            _createBuildingPopupPresenter.InitializeBuildingInfo();
            _createBuildingPopupPresenter.SortBuildingElements();
            var buildingInfo = _createBuildingPopupPresenter._buildings[0];
            _questWidget.RenderElement1(buildingInfo.buildingName, buildingInfo.coinsCountToCreate, 1,
                buildingInfo.itemToCreate);
        }
    }
}
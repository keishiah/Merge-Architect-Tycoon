using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingQuestPanel : MonoBehaviour
{
    [Inject] private StaticDataService data;
    [Inject] private PlayerProgress playerProgress;
    [Inject] private MenuButtonsWidgetController menu;
    [Inject] private CreateBuildingPopupPresenter createBuildingPopupPresenter;
    [Inject]  private ProgressItemPopup progressItemInfo;

    [SerializeField] private BuildingQuestPoint pointPrefab;


    public void OnSceneLoaded()
    {
        playerProgress.Buldings.buildingsInProgress.OnAddBuildingProgress += CheckBuildingsQuest;

        CheckBuildingsQuest();
    }
    public void CheckBuildingsQuest()
    {
        BuildingsData buildings = playerProgress.Buldings;
        List<BuildingInfo> info = data.BuildingInfoDictionary.Values.ToList();
        for (int i  = 0; i < data.BuildingInfoDictionary.Count; i++)
        {
            string buildingName = info[i].buildingName;
            if (buildings.CreatedBuildings.Contains(buildingName) 
                || buildings.buildingsInProgress.BuildingsInProgressDict.ContainsKey(buildingName))
                continue;

            CreateQuestPoints(info[i]);
            return;
        }

        SetBuildingQuestPanelOff();
    }

    private void CreateQuestPoints(BuildingInfo buildingInfo)
    {
        List<MergeItem> itemsToCreate = buildingInfo.itemsToCreate;
        while(itemsToCreate.Count + 1 > transform.childCount)//N items + 1 soft
        {
            Instantiate(pointPrefab, transform);
        }

        //items
        for (int i = 0; i < itemsToCreate.Count; i++)
        {
            BuildingQuestPoint point = transform.GetChild(i).GetComponent<BuildingQuestPoint>();
            int itemsNeedetCount = 1;
            point.NewInitialization(playerProgress, progressItemInfo, itemsNeedetCount, itemsToCreate[i]);
        }

        //soft
        int lastChild = transform.childCount - 1;
        BuildingQuestPoint softPoint = transform.GetChild(lastChild).GetComponent<BuildingQuestPoint>();
        softPoint.NewInitialization(playerProgress, progressItemInfo, buildingInfo.coinsCountToCreate);
    }

    private void SetBuildingQuestPanelOff()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BuildingQuestPoint point = transform.GetChild(i).GetComponent<BuildingQuestPoint>();

            if (point == null)
                point.SetOff();
        }

        GetComponent<Button>().interactable = false;
    }

    public void OpenBuildingPopup()
    {
        if (!IsReadyToBuild())
            return;

        menu.OnMenuButtonClick(MenuButtonsEnum.Build);
        createBuildingPopupPresenter.SelectFirstBuilding();
    }

    private bool IsReadyToBuild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BuildingQuestPoint point = transform.GetChild(i).GetComponent<BuildingQuestPoint>();

            if (point == null || !point.IsDone)
                return false;
        }

        return true;
    }

    private void OnDestroy()
    {
        playerProgress.Buldings.buildingsInProgress.OnAddBuildingProgress -= CheckBuildingsQuest;
    }
}

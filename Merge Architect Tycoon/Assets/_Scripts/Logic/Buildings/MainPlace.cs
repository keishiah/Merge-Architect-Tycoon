using System.Linq;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainPlace : BuildingPlace
{
    public override void InitializeBuilding(int district)
    {
        base.InitializeBuilding(district);
    }

    public override void CreateInMoment()
    {
        if (_staticDataService.BuildingInfoDictionary.Keys.Contains(buildingName))
            _buildingCreator.CreateInMoment(this,
                _staticDataService.BuildingInfoDictionary[buildingName].diamondsCountToSkip);
        else
            _buildingCreator.CreateInMoment(this,
                _staticDataService.MainInfoDictionary[buildingName].diamondsCountToSkip);
    }

    public override void ShowBuildingSprite()
    {
        if (_staticDataService.BuildingInfoDictionary.Keys.Contains(buildingName))
            buildingView.ShowBuildSpriteOnCreate(_staticDataService.BuildingInfoDictionary[buildingName]
                .districtSprite);
        else
            buildingView.ShowBuildSpriteOnCreate(_staticDataService.MainInfoDictionary[buildingName].districtSprite);
    }

    public override void SHowBuilding()
    {
        if (_staticDataService.BuildingInfoDictionary.Keys.Contains(buildingName))
            buildingView.ShowBuildingSprite(_staticDataService.BuildingInfoDictionary[buildingName].districtSprite);
        else
            buildingView.ShowBuildingSprite(_staticDataService.MainInfoDictionary[buildingName].districtSprite);
    }
}
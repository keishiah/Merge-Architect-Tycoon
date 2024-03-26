using System.Collections.Generic;
using _Scripts.Services.Audio;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IStaticDataService
{
    UniTask Initialize();
    Sprite BuildInProgressSprite { get; }
    Dictionary<string, BuildingInfo> BuildingData { get; }
    public List<QuestBase> Quests { get; }
    AudioData AudioData { get; }
    BuildingInfo GetBuildingData(string buildingName);
    DistrictInfo GetDistrictData(int districtId);
}
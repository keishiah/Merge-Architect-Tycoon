using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeBase.Logic.Buildings
{
    public class BuildingStateSetter : MonoBehaviour
    {
        public BuildingPlace buildingPlace;
        public BuildingStateEnum stateToSet;

#if UNITY_EDITOR
        [CustomEditor(typeof(BuildingStateSetter))]
        public class BuildingStateSetterEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                BuildingStateSetter setter = (BuildingStateSetter)target;
                if (GUILayout.Button("Set Building State"))
                {
                    setter.SetBuildingState();
                }
            }
        }
#endif

        public void SetBuildingState()
        {
            if (buildingPlace != null)
            {
                buildingPlace.SetBuildingState(stateToSet);
            }
            else
            {
                Debug.LogError("BuildingPlace reference is missing!");
            }
        }
    }
}
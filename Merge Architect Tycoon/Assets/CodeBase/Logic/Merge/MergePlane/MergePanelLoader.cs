using System;
using UnityEngine;

namespace CodeBase._Gameplay.Merge.MergePlane
{
    public class MergePanelLoader : MonoBehaviour
    {
        public MergeGrid mergeGrid;

        private void Start()
        {
            mergeGrid.InitializeGrid();
        }
    }
}
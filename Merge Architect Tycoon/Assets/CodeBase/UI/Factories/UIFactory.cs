using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Services;
using CodeBase.Services.SceneContextProvider;
using CodeBase.UI.Elements;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Factories
{
    public class UIFactory : IUIFactory
    {
        public Transform _worldCanvasTransform { get; private set; }

        private readonly IAssetProvider _assetProvider;
        private readonly UiPresenter _uiPresenter;
        private readonly SceneContextProvider _sceneContextProvider;

        private GameObject uiRoot;

        public UIFactory(IAssetProvider assetProvider, UiPresenter uiPresenter,
            SceneContextProvider sceneContextProvider)
        {
            _assetProvider = assetProvider;
            _uiPresenter = uiPresenter;
            _sceneContextProvider = sceneContextProvider;
        }


        public async UniTask WarmUp()
        {
            await _assetProvider.Load<GameObject>(AssetPath.UIRoot);
        }

        public async UniTask CreateuiRoot()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetPath.UIRoot);
            var element = _sceneContextProvider.GetCurrentSceneContextInstantiator()
                .InstantiatePrefab(prefab);

            uiRoot = element;
        }

        public async UniTask CreateBuildingPopup()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetPath.CreateBuildingPopup);
            var element = _sceneContextProvider.GetCurrentSceneContextInstantiator()
                .InstantiatePrefab(prefab, uiRoot.transform);
            _uiPresenter._createBuildingPopupPresenter = _sceneContextProvider.Resolve<CreateBuildingPopupPresenter>();
            // _uiPresenter._createBuildingPopupPresenter.SceneConstruct(_sceneContextProvider.Resolve<ItemsCatalogue>(),
            //     _sceneContextProvider.Resolve<BuildingProvider>());
            _uiPresenter.SubscribeUIElementToPresenter(element.GetComponent<CreateBuildingPopup>());
        }

        public async UniTask CreateCoinsUi()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetPath.CoinsUi);
            var element = _sceneContextProvider.GetCurrentSceneContextInstantiator()
                .InstantiatePrefab(prefab);
            _uiPresenter.SubscribeUIElementToPresenter(element.GetComponent<CoinsCounterUi>());
        }


        public void Cleanup()
        {
            // _uiPresenter.Unsubscribe();
        }
    }
}
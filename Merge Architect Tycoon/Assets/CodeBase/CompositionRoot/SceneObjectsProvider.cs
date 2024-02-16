using CodeBase.Services;
using CodeBase.Services.SceneContextProvider;

namespace CodeBase.CompositionRoot
{
    public class SceneObjectsProvider
    {
        private readonly ISceneContextProvider _sceneContextProvider;
        
        public SceneObjectsProvider(ISceneContextProvider sceneContextProvider)
        {
            _sceneContextProvider = sceneContextProvider;
        }

        public void InitializeSceneObjects()
        {

        }

    }
}
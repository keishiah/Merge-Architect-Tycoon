using Zenject;

namespace CodeBase.Services.SceneContextProvider
{
    public class SceneContextProvider : ISceneContextProvider
    {
        private readonly ProjectContext _projectContext;
        private SceneContext _currentContext;

        public SceneContextProvider()
        {
            _projectContext = ProjectContext.Instance;
        }

        public void SetCurrentSceneContext(string sceneName)
        {
            _currentContext = _projectContext.Container.Resolve<SceneContextRegistry>()
                .GetSceneContextForScene(sceneName);
        }

        public IInstantiator GetCurrentSceneContextInstantiator()
        {
            return _currentContext.Container.Resolve<IInstantiator>();
        }

        public T Resolve<T>()
        {
            return _currentContext.Container.Resolve<T>();
        }
    }
}
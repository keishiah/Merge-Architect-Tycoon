using Zenject;

namespace CodeBase.Services.SceneContextProvider
{
    public interface ISceneContextProvider
    {
        void SetCurrentSceneContext(string sceneName);
        IInstantiator GetCurrentSceneContextInstantiator();
        T Resolve<T>();
    }
}
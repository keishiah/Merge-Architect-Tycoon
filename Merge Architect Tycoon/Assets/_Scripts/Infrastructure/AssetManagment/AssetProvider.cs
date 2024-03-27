using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider: IAssetProvider
{
    private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new();

    public async UniTask InitializeAsync()
    {
        await Addressables.InitializeAsync().ToUniTask();
    }

    public async UniTask<T> Load<T>(string address) where T : class
    {
        AsyncOperationHandle handle;

        if (!_assetRequests.TryGetValue(address, out handle))
        {
            handle = Addressables.LoadAssetAsync<T>(address);
            _assetRequests.Add(address, handle);
        }

        await handle.ToUniTask();
        return handle.Result as T;
    }

    public async UniTask<T> Load<T>(AssetReference assetReference) where T : class =>
        await Load<T>(assetReference.AssetGUID);

    public async UniTask<IList<T>> LoadStaticDataByLabel<T>(string label) where T : class
    {
        AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(label, null);
        await handle.ToUniTask();
        IList<T> result = handle.Result;
        Addressables.Release(handle);
        return result;
    }

    public void Cleanup()
    {
        foreach (var assetRequest in _assetRequests)
        {
            Addressables.Release(assetRequest.Value);
        }

        _assetRequests.Clear();
    }
}
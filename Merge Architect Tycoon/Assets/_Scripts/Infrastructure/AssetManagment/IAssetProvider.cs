// using Cysharp.Threading.Tasks;
// using System.Collections.Generic;
//
// namespace CodeBase.Infrastructure.AssetManagment
// {
//     public interface IAssetProvider
//   {
//     UniTask<T> Load<T>(AssetReference monsterDataPrefabReference) where T : class;
//     UniTask<T> Load<T>(string address) where T : class;
//     void Cleanup();
//     UniTask InitializeAsync();
//     UniTask<IList<T>> LoadStaticDataByLabel<T>(string label) where T : class;
//   }
// }
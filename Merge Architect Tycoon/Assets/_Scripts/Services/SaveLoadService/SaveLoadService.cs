using _Scripts.Services.StaticDataService;
using UnityEngine;

namespace _Scripts.Services.SaveLoadService
{
    public enum SaveKey
    {
        Progress,
        Inventory
    }

    public static class SaveLoadService
    {
        public static void Save<T>(SaveKey key, T data)
        {
            PlayerPrefs.SetString(key.ToString(), data.ToJson());
        }

        public static T Load<T>(SaveKey key)
        {
            string stringKey = key.ToString();
            if (!PlayerPrefs.HasKey(stringKey))
                return default(T);

            return PlayerPrefs.GetString(stringKey).ToDeserialized<T>();
        }
    }
}
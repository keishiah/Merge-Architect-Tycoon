using UnityEngine;

public enum SaveKey
{
    //Application
    GameVersion,
    SoundSettings,

    //Gameplay
    Riches,
    Quests,
    Buldings,
    Inventory,
    Truck,

    //Tutorial
    NeedSkipTutorial,
    Tutorial,
}

public static class SaveLoadService
{
    public static void Save<T>(SaveKey key, T data)
    {
        //Debug.Log(data.ToJson());
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
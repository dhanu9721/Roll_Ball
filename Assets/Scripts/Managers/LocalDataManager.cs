using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LOCAL_DATA_KEYS
{
    public const string COMPLETED_LEVELS = "COMPLETED_LEVELS";
    public const string LEVEL = "LEVEL_";
    public const string SELECTED_CONTROL = "SELECTED_CONTROL";
    public const string VOLUME_LEVEL = "VOLUME_LEVEL";
}

public class LocalDataManager
{
    public static LocalDataManager Instance = null;

    public static LocalDataManager GetInstance()
    {
        if (Instance == null)
            Instance = new LocalDataManager();
        return Instance;
    }

    public static void SetPlayerProgressIntType(string key, int data)
    {
        PlayerPrefs.SetInt(key, data);
        PlayerPrefs.Save();
    }
    public static void SetPlayerProgressFloatType(string key, float data)
    {
        PlayerPrefs.SetFloat(key, data);
        PlayerPrefs.Save();
    }
    public static void SetPlayerProgressStringType(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }

    public static string GetPlayerProgressStringType(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetString(key);
        return null;
    }
    public static int GetPlayerProgressIntType(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetInt(key);
        return 0;
    }
    public static float GetPlayerProgressFloatType(string key)
    {
        if (PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetFloat(key);
        return -1f;
    }
}

public class CreatePlayerPrefsProgressData
{
    public int _levelNumber = -1;
    public string _levelName = null;
    public bool _isCompleted = false;
    public int _collectedStars = 0;

    public CreatePlayerPrefsProgressData(int levelNumber, string levelName, bool isCompleted, int collectedStars)
    {
        _levelNumber = levelNumber;
        _levelName = levelName;
        _isCompleted = isCompleted;
        _collectedStars = collectedStars;
    }
}
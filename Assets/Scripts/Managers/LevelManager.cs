using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public int levelNumber = 1;

    void Start()
    {
        // Access level data
        
            Debug.Log("Level " + levels);
       
    }

    public LevelData GetCurrentLevelData()
    {
        return levels[levelNumber-1];
    }
}

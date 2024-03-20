using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // Start is called before the first frame update
    private static DataManager instance = null;
    private int levelNumber = 1;

    public static DataManager GetInstance()
    {
        if(instance == null)
            instance = new DataManager();
        return instance;
    }

    public void setCurrentLevel(int level)
    {
        this.levelNumber = level;
    }
    public int getCurrentLevel()
    {
        return this.levelNumber;
    }
}

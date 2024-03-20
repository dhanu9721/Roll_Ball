using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPrefab : MonoBehaviour
{
    public TextMeshProUGUI LevelNumber;
    private LevelData levelData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(LevelData level)
    {
        levelData = level;
        LevelNumber.text = levelData.levelNumber.ToString();
    }

    public void OnClickLevel()
    {
        GameModel.screenManager.showScreen(ScreenEnum.GamePlayScreen, levelData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

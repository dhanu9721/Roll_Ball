using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AbstractScreen
{
    public LevelManager levelManager;
    public Ground ground;
    // Start is called before the first frame update
    private LevelData currentLevelData = null;
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        GameModel.popUpManager.hide(PopUpType.MiniMap);
        GameState.GetInstance().SetCurrentGameState(GameStateType.GameWin);
        currentLevelData = (LevelData)data;
        DestroyGroundChilds();
        SetPlayerProgress();
        //TODO set level progress data here
    }

    public void DestroyGroundChilds()
    {
        foreach (Transform child in ground.RoomContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ground.PlayerContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in ground.SpikeBallsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetPlayerProgress()
    {
        Debug.Log("on show of game win"+currentLevelData.levelNumber);
        int CompletedLevels = LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.COMPLETED_LEVELS);
        if(currentLevelData.levelNumber >= CompletedLevels + 1)
        {
            LocalDataManager.SetPlayerProgressIntType(LOCAL_DATA_KEYS.COMPLETED_LEVELS, currentLevelData.levelNumber);
        }
        CreatePlayerPrefsProgressData progressData = new CreatePlayerPrefsProgressData(currentLevelData.levelNumber,currentLevelData.levelName,true,3);
        LocalDataManager.SetPlayerProgressStringType(LOCAL_DATA_KEYS.LEVEL+currentLevelData.levelNumber,JsonUtility.ToJson(progressData));
    }

    public override void onHide()
    {
        Debug.Log("on hide of game win");
    }

    public void OnReplay()
    {
        GameModel.screenManager.showScreen(ScreenEnum.GamePlayScreen, currentLevelData);
    }

    public void OnClickNext()
    {
        GameModel.screenManager.showScreen(ScreenEnum.GamePlayScreen, levelManager.GetNextLevelData());
    }

    public void OnClickExit()
    {
        GameModel.screenManager.showScreen(ScreenEnum.MainMenuScreen, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : AbstractScreen
{
    public LevelManager levelManager;
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
        Debug.Log("on show of game win");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : AbstractScreen
{
    //[SerializeField] private GameObject win;
    //[SerializeField] private GameObject Loose;
    //[SerializeField] private Ground _Ground;
    // Start is called before the first frame update
    private LevelData currentLevelData = null;
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        GameModel.popUpManager.hide(PopUpType.MiniMap);
        GameState.GetInstance().SetCurrentGameState(GameStateType.GameOver);
        currentLevelData = (LevelData)data;
        Debug.Log("on show of gameOver");
    }

    public override void onHide()
    {
        Debug.Log("on hide of gameOver");
    }

    public void OnReplay()
    {
        GameModel.screenManager.showScreen(ScreenEnum.GamePlayScreen, currentLevelData);
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

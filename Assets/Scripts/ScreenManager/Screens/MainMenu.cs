using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : AbstractScreen
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        Debug.Log("on show of main menu");
    }

    public override void onHide()
    {
        Debug.Log("on hide of main menu");
    }

    public void OnPlayClick()
    {
        object obj = new { dd = "Dhanajay" };
        GameModel.screenManager.showScreen(ScreenEnum.LevelManagerScreen, obj);
    }

    public void OnClickSetting()
    {
        GameModel.popUpManager.show(PopUpType.setting);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the back button is pressed on Android
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Activate the quit button
            if(GameModel.popUpManager.currentPopUp == PopUpType.QuitGame){
                GameModel.popUpManager.hide(PopUpType.QuitGame, null);
            }
            else
            {
                GameModel.popUpManager.show(PopUpType.QuitGame, null);
            }
        }
    }
}

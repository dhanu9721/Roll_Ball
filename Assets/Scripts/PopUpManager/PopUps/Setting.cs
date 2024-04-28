using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : AbstractPopUp
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        Debug.Log("on show of setting popup");
    }

    public void OnClickExit()
    {
        GameModel.popUpManager.hide(PopUpType.setting);
    }
    public void OnClickSound()
    {
        GameModel.popUpManager.show(PopUpType.sounds);
    } 
    public void OnClickControls()
    {
        GameModel.popUpManager.show(PopUpType.controls);
    }
    public void OnClickQuit()
    {
        GameModel.popUpManager.show(PopUpType.QuitGame);
    }

    public override void onHide()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

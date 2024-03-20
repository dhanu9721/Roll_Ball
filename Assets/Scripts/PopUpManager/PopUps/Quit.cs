using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : AbstractPopUp
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void onShow(object data)
    {
        Debug.Log("on show of setting popup");
    }

    public void OnClickBack()
    {
        GameModel.popUpManager.hide(PopUpType.QuitGame);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public override void onHide()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}

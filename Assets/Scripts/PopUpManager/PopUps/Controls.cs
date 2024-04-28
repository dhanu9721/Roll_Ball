using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlTypes
{
    Joystick = 1,
    ArrowControl,
}


public class Controls : AbstractPopUp
{
    [SerializeField] private GameObject[] ControllerTypes;
    //[SerializeField] private GameObject ArrowControlTickSign;
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void onShow(object data)
    {
        Debug.Log("on show of Control popup");
        var seletctedControl = LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL);

        foreach (var control in ControllerTypes) control.gameObject.SetActive(false);
        if (seletctedControl > 0) ControllerTypes[seletctedControl - 1].gameObject.SetActive(true);
        else
        {
            LocalDataManager.SetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL, (int)ControlTypes.Joystick);
            ControllerTypes[(int)ControlTypes.Joystick - 1].gameObject.SetActive(true);
        }
    }

    public void OnClickExit()
    {
        GameModel.popUpManager.hide(PopUpType.controls);
    }
    public void OnClickJoystickCheck()
    {
        foreach (var control in ControllerTypes) control.gameObject.SetActive(false);
        ControllerTypes[(int)ControlTypes.Joystick -1].SetActive(true);
        LocalDataManager.SetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL, (int)ControlTypes.Joystick);
    }
    public void OnClickArrowControlCheck()
    {
        foreach (var control in ControllerTypes) control.gameObject.SetActive(false);
        ControllerTypes[(int)ControlTypes.ArrowControl -1].SetActive(true);
        LocalDataManager.SetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL, (int)ControlTypes.ArrowControl);
    }

    public override void onHide()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : AbstractScreen
{
    [SerializeField] Ground ground;
    [SerializeField] VibrateEffect vibrateEffect;
    [SerializeField] MiniMap miniMap; 
    [SerializeField] GameObject[] ControllerTypes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        Debug.Log("on show of game play screen");
        LevelData levelData = (LevelData)data;
        vibrateEffect.ResetCameras();
        HandleControl();
        miniMap.init(levelData);
        ground.GenerateRoom(levelData);
    }

    public void HandleControl()
    {
        var seletctedControl = LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL);
       
        foreach (var control in ControllerTypes) control.gameObject.SetActive(false);

        if (seletctedControl > 0) ControllerTypes[seletctedControl-1].gameObject.SetActive(true);
        else
        {
            LocalDataManager.SetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL, (int)ControlTypes.Joystick);
            ControllerTypes[(int)ControlTypes.Joystick - 1].gameObject.SetActive(true);
        } 
    }

    public override void onHide()
    {
        Debug.Log("on hide of GamePlayScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

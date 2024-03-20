using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public static class GameStateType
{
    public const string None = "None";
    public const string Start = "Start";
    public const string Stop = "Stop";
    public const string GameWin = "GameWin";
    public const string GameOver = "GameOver";
}
public class GameState
{
    public static GameState Instance =null;
    private string CurrentState = GameStateType.None;

    public static GameState GetInstance()
    {
        if (Instance == null)
        {
            Instance = new GameState();
        }
        return Instance;
    }

    public void SetCurrentGameState(string gameState)
    {
        CurrentState = gameState;
    }

    public string GetCurrentGameState()
    {
        return CurrentState;
    }
}
public enum ScreenEnum
{
    MainMenuScreen = 0,
    LevelManagerScreen,
    GamePlayScreen,
    GameWinScreen,
    GameOverScreen,
    LoadingScreen,
    NONE = 100,
}

public enum ScreenState
{
    SHOWN = 0,
    HIDDEN = 1,
    NONE = 2,
}

public class ScreenManager : MonoBehaviour
{

    public AbstractScreen[] screens;

    //@property({ type: Enum(ScreenEnum) })
    public ScreenEnum currentScreen = ScreenEnum.MainMenuScreen;

    //@property({ type: Enum(ScreenState) })
    public ScreenState screenState  = ScreenState.NONE;
    // Start is called before the first frame update
    void Awake()
    {
        GameModel.screenManager = this;
    }

    void Update()
    {
    }

    public void showScreen(ScreenEnum screen, object optionalData) {
        enableNewScreen(screen, optionalData);
    }

    private void enableNewScreen(ScreenEnum screen, object optionalData)
    {
        // window.Analytics.viewEvent(ScreenEnum[screen] , ScreenEnum[this.currentScreen]);
        if (currentScreen != ScreenEnum.NONE)
        {
          hideCurrentScreen(screen, optionalData);
        }
        else
        {
          showNextScreen(screen, optionalData);
        }
    }

    private void showNextScreen(ScreenEnum screen, object optionalData)
    {
        int screenIndex = (int)screen;

        screens[screenIndex].gameObject.SetActive(true);
        currentScreen = screen;
        screens[screenIndex].onShow(optionalData);
        screenState = ScreenState.SHOWN;
        return;
    }

    private void hideCurrentScreen(ScreenEnum nextScreen, object nextOptionalData) {
        //var anim = this.screens[this.currentScreen].getComponent('AnimBase');
        var inst = this;
            //if (anim === null) {
        int screenIndex = (int)inst.currentScreen;
        inst.screens[screenIndex].onHide();
        if (inst.screens[screenIndex]) inst.screens[screenIndex].gameObject.SetActive(false);
        inst.screenState = ScreenState.HIDDEN;
        inst.currentScreen = ScreenEnum.NONE;

        if (nextScreen != null)
        {
            inst.showNextScreen(nextScreen, nextOptionalData);
        }
        return;
    }
}

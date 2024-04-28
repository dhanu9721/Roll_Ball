using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sounds : AbstractPopUp
{
    public TextMeshProUGUI volumeLevel;
    public GameObject MusicOnButton;
    public GameObject MusicOffButton;
    public GameObject SfxOnButton;
    public GameObject SfxOffButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void onShow(object data)
    {
        Debug.Log("on show of sounds popup");
    }

    public void OnClickExit()
    {
        GameModel.popUpManager.hide(PopUpType.sounds);
    }

    public void OnClickMusicOn()
    {
        MusicOffButton.SetActive(true);
        MusicOnButton.SetActive(false); 
    } 
    public void OnClickMusicOff()
    {
        MusicOnButton.SetActive(true);
        MusicOffButton.SetActive(false);

    }
    public void OnClickSfxOn()
    {
        SfxOffButton.SetActive(true);
        SfxOnButton.SetActive(false);
    } 
    public void OnClickSfxOff()
    {
        SfxOnButton.SetActive(true);
        SfxOffButton.SetActive(false);
    }

    public void OnClickPlus()
    {
        string volumeText = volumeLevel.text;
        int volume = int.Parse(volumeText);
        if (volume < 100)
        {
            volume += 5;
            volumeLevel.text = ""+volume;
        }
    }
    public void OnClickMinus()
    {
        string volumeText = volumeLevel.text;
        int volume = int.Parse(volumeText);
        if (volume >-1)
        {
            volume -= 5;
            volumeLevel.text = ""+volume;
        }
    }

    public override void onHide()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpType
{
    setting = 0,
    QuitGame,
    MiniMap,
    None = 100,
}

public class PopUpManager : MonoBehaviour
{
    public List<AbstractPopUp> popUps;

    public delegate void CustomFunctionDelegate();

    public PopUpType currentPopUp = PopUpType.None;

    public List<PopUpType> currentOverLayedPopUps = new List<PopUpType>();

    void Awake()
    {
        //window.PopUpManager = this;
        // this.registerCloseBtn();
        //   this.currentOverLayedPopUps.push(PopUpType.None);
        GameModel.popUpManager = this;
    }

    /**
     * Show Pop up
     */
    public void show(PopUpType popUp, object data = null, CustomFunctionDelegate callback = null)
    {
        bool isOpen = currentOverLayedPopUps.IndexOf(popUp) == -1 ? false : true;

        int popUpIndex = (int)popUp;
        //bool isOpen = Array.IndexOf(currentOverLayedPopUps, popUp) == -1 ? false : true;
        //console.log(isOpen, "popup", popUp);

        if (popUp != PopUpType.None && !isOpen)
        {
            //this.currentOverLayedPopUps.push(popUp);
            currentOverLayedPopUps.Add(popUp);
            currentPopUp = popUp;
            popUps[popUpIndex].gameObject.SetActive(true);
            //var anim = popUps[popUpIndex].getComponent(AnimBase);
            string anim = null;
            var inst = this;
            if (anim != null)
            {
                //if (data && data.canPlayAnim == false)
                if (data != null)
                {
                    // inst.popUps[popUp].node.setPosition(-407, 0, 0);
                    // console.log("in the if part")
                    // anim.play("showPopUp", function () {
                    //   if (popUp !== PopUpType.Loading) inst.popUps[popUp].onShow(data);
                    //   if (callback !== null) callback();
                    // }, data.canPlayAnim);
                }
                else
                {
                    //console.log("in the else part")
                    inst.popUps[popUpIndex].onShow(data);
                    /*anim.play(
                      "popUpAnim",
                      function() {
                        // inst.popUps[popUp].onShow(data);
                        // if (callback !== null) callback();
                    },
                true
              );*/
                }
            }
            else
            {
                this.popUps[popUpIndex].onShow(data);
                if (callback != null) callback.Invoke();
            }
        }
    }

    /**
     * Hide PopUp
     */
    public void hide(PopUpType popUp, object data = null, CustomFunctionDelegate callBack = null)
    {

        // if (popUp == 7)
        // {
        // let kenoController = find("Canvas/KenoController");
        //console.log("kenoController");
        //kenoController.getComponent(KenoController).changeState(Keno_Game_States.IDLE);
        //}

        int popUpIndex = (int)popUp;
        bool isOpen = currentOverLayedPopUps.IndexOf(popUp) == -1 ? false : true;
        if (popUp != PopUpType.None && isOpen)
        {
            SpliceList(currentOverLayedPopUps, currentOverLayedPopUps.IndexOf(popUp), 1);
            //currentOverLayedPopUps.splice(this.currentOverLayedPopUps.indexOf(popUp), 1);
            if(currentOverLayedPopUps.Count > 0)
            {
                currentPopUp = currentOverLayedPopUps[currentOverLayedPopUps.Count - 1];
            }
            else currentPopUp = PopUpType.None;
            // var anim = this.popUps[popUp].getComponent(AnimBase);
            string anim = null;
            var inst = this;
            if (anim != null)
            {
                //if (data && data.canPlayAnim == false)
                if (data != null)
                {
                    // inst.popUps[popUp].node.children[2].setPosition(540, 0, 0);
                    // anim.play("hidePopUp", function () {
                    //   inst.popUps[popUp].node.active = false;
                    //   if (popUp !== PopUpType.Loading) inst.popUps[popUp].onHide();
                    //   if (callBack !== null) callBack();
                    // }, data.canPlayAnim);
                }
                else
                {
                    /*anim.play(
                      "popUpAnim", function() {
                        // if (popUp !== PopUpType.Loading) inst.popUps[popUp].onHide();
                        if (callBack != null) callBack();
                        inst.popUps[popUp].node.active = false;
                        console.log("Pop Is Active " + inst.popUps[popUp].node.active);
                    },
                false
                  );*/
                }
            }
            else
            {
                // if (popUp !== PopUpType.Loading) this.popUps[popUp].onHide();
                if (callBack != null) callBack();
                popUps[popUpIndex].gameObject.SetActive(false);
            }
            //   this.popUps[popUp].node.active = false;
        }
        else
        {
            if (callBack != null) callBack.Invoke();
        }
    }

    /**
     * Hide current PopUp
     */
    public void hideCurrentPopUp(CustomFunctionDelegate callBack = null)
    {
        // if (this.currentPopUp == 5) {
        //     this.hide(this.currentPopUp, function() {
        //         this.show(4, null, function() {});
        //     }.bind(this));
        // } else {
        hide(currentPopUp, callBack);
        // }
    }

    /**
     * Hide all Popups
     */
    public void hideAllPopUps()
    {
        while (currentOverLayedPopUps.Count != 0)
        {
            hide(currentPopUp, null);
        }
    }

    void SpliceList<T>(List<T> list, int startIndex, int count, T newItem = default(T))
    {
        // Remove elements starting from the specified index
        list.RemoveRange(startIndex, count);

        // Optionally insert the new item at the specified index
        if (!EqualityComparer<T>.Default.Equals(newItem, default(T)))
        {
            list.Insert(startIndex, newItem);
        }
    }
}
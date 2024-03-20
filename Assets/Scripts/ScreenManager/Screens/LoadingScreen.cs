using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : AbstractScreen
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void onShow(object data)
    {
        Debug.Log("on show of loading screen");
    }

    public override void onHide()
    {
        Debug.Log("on hide of Loading screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

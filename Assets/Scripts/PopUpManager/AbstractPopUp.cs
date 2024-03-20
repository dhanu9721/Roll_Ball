using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    object Data;
    void Start()
    {

    }

    public abstract void onShow(object data);

    public abstract void onHide();

    // Update is called once per frame
    void Update()
    {
        
    }
}

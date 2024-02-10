using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public TeleportPoint SiblingTeleportPoint;
    public string myTag;
    public bool isBallOutSideTeleportPoint = true;
    void Start()
    {
        
    }

    public void init(string tag)
    {
        myTag = tag;
    }

    public void SetSiblingTeleportPoint(TeleportPoint siblingTeleportPoint)
    {
        SiblingTeleportPoint = siblingTeleportPoint;
    }

    public Transform SetBallToSiblingPosition()
    {
        SiblingTeleportPoint.isBallOutSideTeleportPoint = false;
        isBallOutSideTeleportPoint = true;
        return SiblingTeleportPoint.gameObject.transform;
    }
    public bool checkBallForTeleport()
    {
        return isBallOutSideTeleportPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

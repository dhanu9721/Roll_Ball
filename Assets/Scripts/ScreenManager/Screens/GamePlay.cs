using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : AbstractScreen
{
    [SerializeField] Ground ground;
    [SerializeField] MiniMap miniMap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void onShow(object data)
    {
        Debug.Log("on show of game play screen");
        LevelData levelData = (LevelData)data;
        miniMap.init(levelData);
        ground.GenerateRoom(levelData);
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

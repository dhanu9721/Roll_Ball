using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private MapController mapController;
    private LevelData currentLevelData = null;
    void Start()
    {

    }

    public void init(LevelData levelData){
        currentLevelData = levelData;
    }

    public void OnClickMap()
    {
        GameModel.popUpManager.show(PopUpType.MiniMap,currentLevelData);
        //mapController.gameObject.SetActive(!mapController.gameObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

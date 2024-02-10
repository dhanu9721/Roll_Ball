using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private GameOver _GameOver;
    public LocalPlayerData[] localPlayerData;
    public GameObject player;
    public Transform PlayerContainer;
    public Transform healthContainer;
    public bool isLocalPlayer = true;
    // Start is called before the first frame update

    private void Start()
    {
        Debug.Log("ewfvjkf : " + localPlayerData);
    }

    public void Restart()
    {
        foreach (Transform child in PlayerContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in healthContainer)
        {
            Destroy(child.gameObject);
        }
        _GameOver.gameObject.SetActive(false);
    }
    public void GeneratePlayer(LevelData levelData,List<TeleportPoint> teleportPointsList,Transform myStartPoint, Transform MyGAmeOverPoint)
    {
        GameObject instance = Instantiate(player, PlayerContainer);
        PlayerData playerData = GetPlayerData(levelData);
        Debug.Log("my pos : " + myStartPoint.position);
       // instance.transform.position = myStartPoint.position;
        instance.transform.localPosition = myStartPoint.position;
        Player playerComponent = instance.GetComponent<Player>();
        //healthManager.AddHealth(playerData.health);
        playerComponent.init(playerData,myStartPoint, teleportPointsList, _fixedJoystick, healthContainer,_GameOver);
        instance.transform.parent = PlayerContainer;
        //playerComponent.SetTransformOfObject(instance, PlayerContainer, levelData.startPoints[0]);
    }

    public PlayerData GetPlayerData(LevelData levelData)
    {
        PlayerData playerData = new PlayerData();
        if (isLocalPlayer)
        {
            LocalPlayerData currentLocalPlayerData = GetLocalPlayerData();
            Debug.Log("dg : "+localPlayerData);
            playerData.currentLevelData = levelData;
            playerData.playerObject = currentLocalPlayerData.playerObject;
            playerData.playerLevel = 1;
    
        }
        else
        {
            playerData.currentLevelData = levelData;
            playerData.playerLevel = 1;
        }
        return playerData;
    }

    public LocalPlayerData GetLocalPlayerData()
    {
        return localPlayerData[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

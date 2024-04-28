using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject Room;
    public GameObject SpikeBall;

    public Transform RoomContainer;
    public Transform PlayerContainer;
    public Transform SpikeBallsContainer;

    public GameObject Boundaries;
 
    public PlayerManager playerManager;
    //public LevelManager2 levelManager;

    public List<TeleportPoint> teleportPortPointsList = new List<TeleportPoint>();

    public float rotationSpeed = 1.5f; // Adjust the speed as needed
    public float tiltAngle = 1.5f;

    void Start()
    {
        //GenerateRoom();
    }

    public void Restart()
    {
        foreach(Transform child in RoomContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform boundaryChild in Boundaries.transform)
        {
            boundaryChild.gameObject.SetActive(false);
        }
        playerManager.Restart();
        gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
       // gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        //GenerateRoom();
    }
    public void GenerateRoom(LevelData levelData)
    {
        // Instantiate the prefab
        // LevelData levelData = levelManager.GetCurrentLevelData();
        // Debug.Log(levelData.rooms.startRoom.ToString()) ;
        Restart();
        SetGroundBoundary(levelData);
        GenerateSpikeBalls(levelData);
        GenerateRoomEntities(levelData);
        GameState.GetInstance().SetCurrentGameState(GameStateType.Start);
       
    }

    public void GenerateSpikeBalls(LevelData levelData)
    {
        foreach (var SpikeBallPos in levelData.spikeBallsPosition)
        {
            GameObject SpikeBallInstance = Instantiate(SpikeBall, SpikeBallsContainer);
            SpikeBallInstance.transform.position = new Vector3(SpikeBallPos.x, SpikeBallPos.y, SpikeBallPos.z);
        }
    }

    public void SetGroundBoundary(LevelData levelData)
    {
        Debug.Log(levelData.groundShape);
        GameObject levelBoundary = Boundaries.transform.Find(levelData.groundShape).gameObject;
        Debug.Log(levelBoundary);
        levelBoundary.SetActive(true);
        Boundaries.transform.localScale = new Vector3(levelData.groundScale.x, levelData.groundScale.y, levelData.groundScale.z);
    }


    public void GenerateRoomEntities(LevelData levelData)
    {
        List<SingleRoom> commonRooms = levelData.rooms.CommonRooms;
        SingleRoom startRoom = levelData.rooms.startRoom;
        SingleRoom gameOverRoom = levelData.rooms.gameOverRoom;

        Room startRoomComponent = GenerateSingleRoom(startRoom);
        GameObject gameStartPointObject = startRoomComponent.GenerateGameStartPoint(startRoom);
        if (commonRooms.Count > 0)
        {
            foreach (var item in commonRooms)
            {
                GenerateSingleRoom(item);
            }
        }
        Room gameOverRoomComponent = GenerateSingleRoom(gameOverRoom);
        GameObject gameOverPointObject =  gameOverRoomComponent.GenerateGameOverPoint(gameOverRoom);
        SetSiblingTeleportPoint();
        playerManager.GeneratePlayer(levelData, teleportPortPointsList, gameStartPointObject.transform,gameOverPointObject.transform);

    }

    Room GenerateSingleRoom(SingleRoom singleRoom)
    {
        List<RoomEntityObject> walls = singleRoom.walls;
        List<RoomEntityObject> teleportPoints = singleRoom.teleportPoints;

        GameObject instance = Instantiate(Room, RoomContainer);
        instance.transform.parent = RoomContainer;
        Room roomComponent = instance.GetComponent<Room>();
        if (walls.Count > 0)
        {
            roomComponent.GenerateWalls(walls);
        }
        if (teleportPoints.Count > 0)
        {
            roomComponent.GenerateTeleportPoints(teleportPoints,teleportPortPointsList);
        }
        return roomComponent;
    }

    public void SetSiblingTeleportPoint()
    {
        foreach (var teleportPoint1 in teleportPortPointsList)
        {
            foreach (var teleportPoint2 in teleportPortPointsList)
            {
                if (teleportPoint1 != teleportPoint2 && teleportPoint1.myTag == teleportPoint2.myTag)
                {
                    teleportPoint1.SetSiblingTeleportPoint(teleportPoint2);
                    teleportPoint2.SetSiblingTeleportPoint(teleportPoint1);
                }
            }
        }
    }

    void Update()
    {
        if(GameState.GetInstance().GetCurrentGameState() == GameStateType.Start)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            float tiltAmountX = Mathf.Sin(Time.time * rotationSpeed) * tiltAngle;
            float tiltAmountZ = Mathf.Cos(Time.time * rotationSpeed) * tiltAngle;

            Quaternion targetRotation = Quaternion.Euler(tiltAmountX, transform.rotation.eulerAngles.y, tiltAmountZ);
            //Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, tiltAmountX, tiltAmountZ);
            //Quaternion targetRotation = Quaternion.Euler(tiltAmountX, tiltAmountZ, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static MirzaBeig.ParticleSystems.Demos.DemoManager;

public class JsonLevelCreator : MonoBehaviour
{
    public GameObject[] LevelPrefabs;
    //private LevelData currentLevelData;
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }
    public LevelData GroundData(GameObject currentGround,int index)
    {
        //foreach (var level in LevelPrefabs)
        //{
            LevelData currentLevelData = new LevelData();
            Transform GroundShapes = currentGround.transform.Find("All Shapes");
            currentLevelData.levelNumber = index+1;
            currentLevelData.levelName = "Level_" + (index+1);
            currentLevelData.timeLimit = 10;
            currentLevelData.groundScale = new Co_Ordinates(GroundShapes.localScale);

            foreach (Transform groundShape in GroundShapes)
            {
                if (groundShape.gameObject.activeSelf)
                {
                    currentLevelData.groundShape = groundShape.name;
                }
            }
            currentLevelData.rooms = new Rooms();
            RoomContainerData(currentGround.transform.GetChild(1), currentLevelData);
            return currentLevelData;
        //}
    }
    public void RoomContainerData(Transform RoomsContainer, LevelData currentLevelData)
    {
        currentLevelData.rooms.startRoom = RoomData(RoomsContainer.GetChild(0));

        foreach (Transform commonRoom in RoomsContainer.GetChild(1))
        {
            currentLevelData.rooms.CommonRooms.Add(RoomData(commonRoom));
        }

        currentLevelData.rooms.gameOverRoom = RoomData(RoomsContainer.GetChild(2));
    }

    public SingleRoom RoomData(Transform RoomTransform)
    {
        SingleRoom singleRoom = new SingleRoom();
        Transform WallsContainer = RoomTransform.Find("WallsContainer");

        foreach (Transform wall in WallsContainer)
        {

            //currentLevelData.groundShape = groundShape.name;
            RoomEntityObject WallData = new RoomEntityObject();
            WallData.tag = wall.tag;
            WallData.materialColor = null;
            WallData.transform = CreateTransformData(wall);

            singleRoom.walls.Add(WallData);
        }
        Transform TeleportPointsContainer = RoomTransform.Find("TeleportPointsContainer");

        foreach (Transform teleportPoint in TeleportPointsContainer)
        {

            RoomEntityObject portData = new RoomEntityObject();
            portData.tag = teleportPoint.tag;
            portData.materialColor = teleportPoint.tag;
            portData.transform = CreateTransformData(teleportPoint);
            singleRoom.teleportPoints.Add(portData);
        }

        Transform GameOverPointContainer = RoomTransform.Find("GameOverPointContainer");

        foreach (Transform GameOverPoint in GameOverPointContainer)
        {

            RoomEntityObject gameOverData = new RoomEntityObject();
            gameOverData.tag = GameOverPoint.tag;
            gameOverData.materialColor = null;
            gameOverData.transform = CreateTransformData(GameOverPoint);
            singleRoom.gameOverPoint = gameOverData;
        }
        Transform GameStartPointContainer = RoomTransform.Find("GameStartPointContainer");

        foreach (Transform GameStartPoint in GameStartPointContainer)
        {

            RoomEntityObject gameStartData = new RoomEntityObject();
            gameStartData.tag = GameStartPoint.tag;
            gameStartData.materialColor = null;
            gameStartData.transform = CreateTransformData(GameStartPoint);
            singleRoom.startPoints.Add(gameStartData);
        }

        return singleRoom;
    }

    public RoomEntityObjectTransform CreateTransformData(Transform transformObject)
    {
        RoomEntityObjectTransform entityTransform = new RoomEntityObjectTransform();
        entityTransform.Scale = new Co_Ordinates(transformObject.localScale);
        entityTransform.Position = new Co_Ordinates(transformObject.position);
        entityTransform.Rotation = new Co_Ordinates(transformObject.eulerAngles);
        return entityTransform;
    }

    public void CreateLevel()
    {
        Debug.Log("Level create");
        for (int levelNumber =0; levelNumber < LevelPrefabs.Length; levelNumber++)
        { 
            string json = JsonUtility.ToJson(GroundData(LevelPrefabs[levelNumber],levelNumber));

            // Write the JSON data to a file
            string folderPath = Application.dataPath + "/Scripts/Levels";
            string fileName = "Level_" + (levelNumber + 1) + ".json";
            string filePath = Path.Combine(folderPath, fileName);
            File.WriteAllText(filePath, json);
            Debug.Log("level created");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

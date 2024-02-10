using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Co_Ordinates
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class RoomEntityObjectTransform
{
    public Co_Ordinates Position;
    public Co_Ordinates Rotation;
    public Co_Ordinates Scale;
}

[System.Serializable]
public class RoomEntityObject
{
    public string tag;
    public string materialColor;
    public RoomEntityObjectTransform transform;
}

[System.Serializable]
public class SingleRoom
{
    //public RoomEntityObjectTransform roomTransform;
    public List<RoomEntityObject> walls;
    public List<RoomEntityObject> teleportPoints;
    public RoomEntityObject gameOverPoint;
    public List<RoomEntityObject> startPoints;
}

[System.Serializable]
public class Rooms
{
    public SingleRoom startRoom;
    public List<SingleRoom> CommonRooms;
    public SingleRoom gameOverRoom;
}


[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelName;
    public float timeLimit;
    public Rooms rooms;
}

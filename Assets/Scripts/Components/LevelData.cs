using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Co_Ordinates
{
    public float x;
    public float y;
    public float z;
    public Co_Ordinates(Vector3 Vec3Data)
    {
        x = Vec3Data.x; y = Vec3Data.y; z = Vec3Data.z;
    }
   
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
    public List<RoomEntityObject> walls = new List<RoomEntityObject>();
    public List<RoomEntityObject> teleportPoints = new List<RoomEntityObject>();
    public RoomEntityObject gameOverPoint;
    public List<RoomEntityObject> startPoints = new List<RoomEntityObject>();
}

[System.Serializable]
public class Rooms
{
    public SingleRoom startRoom;
    public List<SingleRoom> CommonRooms = new List<SingleRoom>();
    public SingleRoom gameOverRoom;
}


public class LevelData
{
    public int levelNumber;
    public string levelName;
    public string groundShape;
    public Co_Ordinates groundScale;
    public float timeLimit;
    public Rooms rooms;
}
/*[CreateAssetMenu(fileName = "NewLevelData", menuName = "Level Data", order = 51)]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public string levelName;
    public string groundShape;
    public Co_Ordinates groundScale;
    public float timeLimit;
    public Rooms rooms;
}*/

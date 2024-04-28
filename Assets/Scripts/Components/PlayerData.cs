using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerLevel;
    public int health = 1;
    public Vector3 localScale = new Vector3(10f, 10f, 10f);
    public Quaternion localRotation = Quaternion.Euler(0f, 0f, 0f);
    public float speed = 25f;
    public LevelData currentLevelData;
    public PlayerObject playerObject;
}

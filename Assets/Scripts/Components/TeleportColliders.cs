using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderType
{
    public const string Wall = "Wall";
    public const string Boundary = "Boundary";
    public const string SpikeBall = "SpikeBall";

    public const string Ground = "Ground";

    public const string GameOver = "GameOver";
    public const string Red = "Red";
    public const string Blue = "Blue";
    public const string Yellow = "Yellow";
    public const string Green = "Green";
    public const string Maroon = "Maroon";
    public const string Gray = "Gray";
    public const string Aqua = "Aqua";
    public const string Brown = "Brown";
    public const string Purple = "Purple";
    public const string Lime = "Lime";
}
public class TeleportColliders
{
    public static bool CheckCollider(string colliderType)
    {
        switch (colliderType)
        {
            case ColliderType.Aqua:
            case ColliderType.Lime:
            case ColliderType.Maroon:
            case ColliderType.Blue:
            case ColliderType.Purple:
            case ColliderType.Green:
            case ColliderType.Gray:
            case ColliderType.Red:
            case ColliderType.Brown:
            case ColliderType.Yellow:
                return true;
            default: return false;
        }
    }
}

public class WallColliders
{
    public static bool CheckCollider(string colliderType)
    {
        switch (colliderType)
        {
            case ColliderType.Wall:
                return true;
            case ColliderType.Boundary:
                return true;
            case ColliderType.SpikeBall:
                return true;
            default: return false;
        }
    }
}

public class GameOverColliders
{
    public static bool CheckCollider(string colliderType)
    {
        switch (colliderType)
        {
            case ColliderType.GameOver:
                return true;
            default: return false;
        }
    }
}

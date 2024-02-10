using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject Wall;
    public GameObject TeleportPoint;
    public GameObject GameOverPoint;
    public GameObject GameStartPoint;

    public Transform WallContainer;
    public Transform TeleportPointContainer;
    public Transform GameStartPointContainer;
    public Transform GameOverPointContainer;

    public Material teleportMaterial;
    public Material wallMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GenerateWalls(List<RoomEntityObject> walls)
    {
          if(walls.Count > 0)
          {
               foreach (var wall in walls)
               {
                    GameObject instance = Instantiate(Wall, WallContainer);
                //instance.transform.parent = WallContainer;
                    SetTransformOfObject(instance, WallContainer, wall.transform);
                    setTagAndMaterial(instance, wallMaterial, wall);
            }
          }
    }

    public void GenerateTeleportPoints(List<RoomEntityObject> teleportPoints,List<TeleportPoint> teleportPortPointsList)
    {
        if (teleportPoints.Count > 0)
        {
            foreach (var teleportPoint in teleportPoints)
            {
                GameObject instance = Instantiate(TeleportPoint, TeleportPointContainer);

                TeleportPoint teleportComponent = instance.GetComponent<TeleportPoint>();
                teleportComponent.init(teleportPoint.tag);
                SetTransformOfObject(instance, TeleportPointContainer, teleportPoint.transform);
                setTagAndMaterial(instance, teleportMaterial, teleportPoint);
                teleportPortPointsList.Add(teleportComponent);
            }
        }
    }

    void SetTransformOfObject(GameObject instanceObject,Transform objectContainer, RoomEntityObjectTransform objectTransform)
    {
        var position = objectTransform.Position;
        var rotation = objectTransform.Rotation;
        var scale = objectTransform.Scale;

        instanceObject.transform.parent = objectContainer;
        instanceObject.transform.position = new Vector3(position.x, position.y, position.z); // Set position to match the spawn node
        instanceObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // Example scaling
        instanceObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z); // Example rotation
    }

    void setTagAndMaterial(GameObject instanceObject,Material objectMaterial, RoomEntityObject objectData)
    {
        if (objectData.tag != null)
        {
            instanceObject.tag = objectData.tag;
        }
        /**if(objectMaterial != null)
        {
            instanceObject.GetComponent<Renderer>().material = objectMaterial;
            objectMaterial.color = GetColorByName(objectData.materialColor);
        }*/

        Renderer objectRenderer = instanceObject.GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // Check if objectMaterial is not null before assigning it
            if (objectMaterial != null)
            {
               Material newMaterial = new Material(objectMaterial);

                // Assuming GetColorByName returns a Color value
                Color colorByName = GetColorByName(objectData.materialColor);

                // Set the color of the material
                newMaterial.color = colorByName;
                objectRenderer.material = newMaterial;
            }
            else
            {
                Debug.LogWarning("objectMaterial is null. Ensure it is assigned.");
            }
        }
    }

    public GameObject GenerateGameStartPoint(SingleRoom gameStartRoom)
    {
        GameObject instance = Instantiate(GameStartPoint, GameStartPointContainer);
        SetTransformOfObject(instance, GameStartPointContainer, gameStartRoom.startPoints[0].transform);
        return instance;
    }
    public GameObject GenerateGameOverPoint(SingleRoom gameOverRoom)
    {
        GameObject instance = Instantiate(GameOverPoint, GameOverPointContainer);
        SetTransformOfObject(instance, GameOverPointContainer, gameOverRoom.gameOverPoint.transform);
        setTagAndMaterial(instance, teleportMaterial, gameOverRoom.gameOverPoint);
        return instance;
    }

    private Color GetColorByName(string name)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(name, out color))
        {
            return color;
        }
        else
        {
            Debug.LogWarning("Invalid color name: " + name);
            return Color.white; // Return default color (white) if the color name is invalid
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ColorLists()
    {
        string[] colorNames = {
        "Black", "Silver", "Gray", "White", "Maroon", "Red", "Purple", "Fuchsia",
        "Green", "Lime", "Olive", "Yellow", "Navy", "Blue", "Teal", "Aqua",
        };
        // Add more color names from the supported list
        // For brevity, I didn't include all color names here
    }
}

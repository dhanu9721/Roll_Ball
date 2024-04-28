using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public TeleportPoint SiblingTeleportPoint;
    [SerializeField]private ParticleSystem pSystem;
    [SerializeField]private ParticleSystem p2System;
    [SerializeField]private GameObject circle;
    [SerializeField]private Material circleMaterial;

    private ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule;
    public string myTag;
    public string endColorName = "#FFFFFF";
    public bool isBallOutSideTeleportPoint = true;
    void Start()
    {

    }

    public void init(RoomEntityObject teleportPointData)
    {
        myTag = teleportPointData.tag;
        gameObject.tag = myTag;
        setPortalColor(teleportPointData);
    }

    public void setPortalColor(RoomEntityObject teleportPointData)
    {
        colorOverLifetimeModule = p2System.colorOverLifetime;
        //colorOverLifetimeModule.startColor = pSystem.startColor;

        // Set the start and end colors
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(GetColorByName(teleportPointData.materialColor), 0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f) }
        );
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(GetColorByName(teleportPointData.materialColor), 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 1f) }
        );

        // Set the color gradient
        colorOverLifetimeModule.color = gradient;
        pSystem.startColor = GetColorByName(teleportPointData.materialColor);


        setCircleColor(teleportPointData);
    }

    public void setCircleColor(RoomEntityObject teleportPointData)
    {
        Renderer objectRenderer = circle.GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            // Check if objectMaterial is not null before assigning it
            if (circleMaterial != null)
            {
                Material newMaterial = new Material(circleMaterial);

                // Assuming GetColorByName returns a Color value
                Color colorByName = GetColorByName(teleportPointData.materialColor);

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

    public void SetSiblingTeleportPoint(TeleportPoint siblingTeleportPoint)
    {
        SiblingTeleportPoint = siblingTeleportPoint;
    }

    public Transform SetBallToSiblingPosition()
    {
        SiblingTeleportPoint.isBallOutSideTeleportPoint = false;
        isBallOutSideTeleportPoint = true;
        return SiblingTeleportPoint.gameObject.transform;
    }
    public bool checkBallForTeleport()
    {
        return isBallOutSideTeleportPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

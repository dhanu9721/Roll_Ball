using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapDragger : MonoBehaviour
{
    public Camera renderCamera;
    public float dragSpeed = 50f;
    public Vector2 mapSize = new Vector2(950.4f, 534.6f); // Size of your map frame

    private bool isDragging = false;
    private Vector2 dragOrigin;

    public TextMeshProUGUI text;

   /* void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if touch is within map bounds
            if (touch.phase == TouchPhase.Began && IsTouchWithinMapBounds(touch.position))
            {
                isDragging = true;
                dragOrigin = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }

        // If dragging, move the render texture
        if (isDragging)
        {
            Vector3 pos = renderCamera.transform.position;
            Vector2 touchDelta = (Vector2)renderCamera.ScreenToViewportPoint(dragOrigin - Input.GetTouch(0).position);
            pos.x = Mathf.Clamp(pos.x + touchDelta.x * dragSpeed, -mapSize.x / 2f + renderCamera.orthographicSize * renderCamera.aspect, mapSize.x / 2f - renderCamera.orthographicSize * renderCamera.aspect);
            pos.y = Mathf.Clamp(pos.y + touchDelta.y * dragSpeed, -mapSize.y / 2f + renderCamera.orthographicSize, mapSize.y / 2f - renderCamera.orthographicSize);
            renderCamera.transform.position = pos;
            dragOrigin = Input.GetTouch(0).position;
            text.text = "Camera Drag" + renderCamera.gameObject.transform.position.x + " " + renderCamera.gameObject.transform.position.y;
        }
    }*/

    // Function to check if touch is within map bounds
    /*private bool IsTouchWithinMapBounds(Vector2 touchPosition)
    {
        Vector2 worldPoint = renderCamera.ScreenToWorldPoint(touchPosition);
        Vector2 mapBoundsMin = new Vector2(-mapSize.x / 2f, -mapSize.y / 2f);
        Vector2 mapBoundsMax = new Vector2(mapSize.x / 2f, mapSize.y / 2f);

        return worldPoint.x >= mapBoundsMin.x && worldPoint.x <= mapBoundsMax.x &&
               worldPoint.y >= mapBoundsMin.y && worldPoint.y <= mapBoundsMax.y;
    }*/

    void Update()
    {
        // Check for touch input on the parent GameObject
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchWithinMapBounds(touch.position))
            {
                isDragging = true;
                dragOrigin = touch.position;
            }
            else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isDragging)
            {
                isDragging = false;
            }
        }

        // If dragging, move the render texture
        if (isDragging)
        {
            Vector3 pos = renderCamera.transform.position;
            Vector2 touchDelta = (Vector2)renderCamera.ScreenToViewportPoint(dragOrigin - Input.GetTouch(0).position);
            pos.x = Mathf.Clamp(pos.x + touchDelta.x * dragSpeed, -mapSize.x / 2f + renderCamera.orthographicSize * renderCamera.aspect, mapSize.x / 2f - renderCamera.orthographicSize * renderCamera.aspect);
            pos.y = Mathf.Clamp(pos.y + touchDelta.y * dragSpeed, -mapSize.y / 2f + renderCamera.orthographicSize, mapSize.y / 2f - renderCamera.orthographicSize);
            renderCamera.transform.position = pos;
            dragOrigin = Input.GetTouch(0).position;
            text.text = "Camera Drag" + renderCamera.gameObject.transform.position.x + " " + renderCamera.gameObject.transform.position.y;
        }
    }

    // Function to check if touch is within map bounds
    private bool IsTouchWithinMapBounds(Vector2 touchPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, touchPosition, null, out localPoint);

        return localPoint.x >= -mapSize.x / 2f && localPoint.x <= mapSize.x / 2f &&
               localPoint.y >= -mapSize.y / 2f && localPoint.y <= mapSize.y / 2f;
    }
}


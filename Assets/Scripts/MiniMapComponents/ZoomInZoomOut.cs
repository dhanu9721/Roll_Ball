using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ZoomInZoomOut : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 108f;//TODO It should be dynamic according to the ground size

    private float maxDragSpeed = 0.4f;
    public float dragSpeed = 0.4f;
    private Vector3 dragOrigin;
    private Vector3 mapSize = new Vector3(800.0f, 800.0f, 0);
    private bool isFirstTouchOffsetOnMap = false;
    private LevelData CurrentLevelData = null;

    private bool isDragging = false;

    [SerializeField] private Camera mainCamera;
    //Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    //[SerializeField]
    //float zoomModifierSpeed = 0.1f;

    //public TextMeshProUGUI text;

    // Use this for initialization
    void Start()
    {
        mainCamera.orthographicSize = maxZoom;
        mainCamera.transform.localPosition = convertToDesignTouch(mainCamera.transform.localPosition);

        //Collider collider = GetComponent<Collider>();
        //mapSize = collider.bounds.size;
        //mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
                mainCamera.orthographicSize += zoomModifier;
            if (touchesPrevPosDifference < touchesCurPosDifference)
                mainCamera.orthographicSize -= zoomModifier;

        }



        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        dragSpeed = (maxDragSpeed / maxZoom) * mainCamera.orthographicSize;

        mapDragger();
        //text.text = "Camera size " + mainCamera.orthographicSize;

    }

    public void mapDragger()
    {
        if (IsTouchWithinMapBounds(Input.mousePosition) && Input.touchCount == 1)
        {
            isDragging = true;
        }
        else
        {
            isDragging = false;
            isFirstTouchOffsetOnMap = false;
        }

        if(isDragging)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
               // dragOrigin = Input.mousePosition;
            //}
            if (!isFirstTouchOffsetOnMap)
            {
                dragOrigin = Input.mousePosition;
                isFirstTouchOffsetOnMap = true;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 difference = (Input.mousePosition - dragOrigin) * dragSpeed;
                Vector3 move = new Vector3(-difference.x, 0f, -difference.y);
                mainCamera.transform.Translate(move, Space.World);
                dragOrigin = Input.mousePosition; // Update drag origin for smooth dragging
            }
        }
        
    }

    private bool IsTouchWithinMapBounds(Vector2 touchPosition)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, touchPosition, null, out localPoint);

        return localPoint.x >= -mapSize.x / 2f && localPoint.x <= mapSize.x / 2f &&
               localPoint.y >= -mapSize.y / 2f && localPoint.y <= mapSize.y / 2f;
    }

    public Vector3 convertToDesignTouch(Vector3 data){
        float designWidth = Screen.width;
        float designHeight = Screen.height;
        float actualScreenWidth = 1920.0f;
        float actualScreenHeight = 1080.0f;
        float scaleFactorX = designWidth/ actualScreenWidth;
        float scaleFactorY = designHeight/ actualScreenHeight;
        float designTouchX = data.x * scaleFactorX;
        float designTouchY = data.y * scaleFactorY;
        return new Vector3(designTouchX, designTouchY, 0);
    }
}
using System;
using UnityEngine;
using System.Collections;

public static class DIRECTION
{
    public const string RIGHT = "RIGHT";
    public const string LEFT = "LEFT";
    public const string UP = "UP";
    public const string DOWN = "DOWN";
}

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Player MovingObject;
    [SerializeField] private GameObject ArrowObject;

    private float _speed = 3.0f;
    private Vector3 ControlSize = new Vector3(300, 300, 0);
    private bool _isTouchInside = false;
    private bool _isMouseDown = false;
    private Vector3 _currentTouchPosition = new Vector3();
    //private string _currentDirection = null;
        private Vector3 _currentDirection = new Vector3();
    // Start is called before the first frame update

    private Coroutine schedulerCoroutine;

    private void Awake()
    {
        SetPositionAccordingToView();
    }
    void Start()
    {
    }



    public void SetMovingObject(Player movingObject)
    {
        MovingObject = movingObject;
    }

    /*private string calculateDirection(Vector3 touchLoc)
    {
        Vector3 parentPosition = transform.position;
        Vector3 offsetVec = new Vector3(touchLoc.x - parentPosition.x, touchLoc.y - parentPosition.y, touchLoc.z);

        //Debug.Log("tuch loc 2 " + touchLoc + "parentPosition : "+ parentPosition);
        Vector3 direction = Vector3.Normalize(offsetVec);

        string dir; 
        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            // Horizontal direction
            dir = direction.x > 0 ? DIRECTION.RIGHT : DIRECTION.LEFT;
        }
        else
        {
            // Vertical direction
            dir = direction.y > 0 ? DIRECTION.UP : DIRECTION.DOWN;
        }
        //SetBodyPosition(direction);
        return dir;
    }*/
    private Vector3 calculateDirection(Vector3 touchLoc)
    {
        Vector3 parentPosition = transform.position;
        Vector3 offsetVec = new Vector3(touchLoc.x - parentPosition.x, touchLoc.y - parentPosition.y, touchLoc.z);

        //Debug.Log("tuch loc 2 " + touchLoc + "parentPosition : "+ parentPosition);
        Vector3 direction = Vector3.Normalize(offsetVec);

        
        //SetBodyPosition(direction);
        return direction;
    }

    private void moveBall(string direction)
    {
        Vector3 movingObjectPosition = MovingObject.transform.localPosition;
        Vector3 newPosition = new Vector3();
        //Debug.Log("Direction : "+ direction);
        switch (direction)
        {
            case DIRECTION.UP:
                newPosition = new Vector3(movingObjectPosition.x, movingObjectPosition.y + this._speed, movingObjectPosition.z);
                break;
            case DIRECTION.DOWN:
                newPosition = new Vector3(movingObjectPosition.x, movingObjectPosition.y - this._speed, movingObjectPosition.z);
                break;
            case DIRECTION.LEFT:
                newPosition = new Vector3(movingObjectPosition.x - this._speed, movingObjectPosition.y, movingObjectPosition.z);
                break;
            case DIRECTION.RIGHT:
                newPosition = new Vector3(movingObjectPosition.x + this._speed, movingObjectPosition.y, movingObjectPosition.z);
                break;
        }
        MovingObject.transform.localPosition = newPosition;
      
    }

    public void SetBodyPosition(Vector3 ObjectPosition)
    {
        MovingObject._rigidBody.velocity = new Vector3(ObjectPosition.x * MovingObject.playerData.speed, MovingObject._rigidBody.velocity.y, ObjectPosition.y * MovingObject.playerData.speed);

        //_rigidBody.AddForce(movement, ForceMode.VelocityChange);
        //transform.localScale = playerData.localScale;
        //transform.rotation = Quaternion.EulerRotation(0f, 0f, 0f);
        //transform.localRotation = playerData.localRotation;
        Debug.Log("hdf");
        if (ObjectPosition.x != 0 || ObjectPosition.y != 0)
        {
            MovingObject.transform.rotation = Quaternion.LookRotation(MovingObject._rigidBody.velocity);
        }
    }

    public void SetPositionAccordingToView()
    {
        Vector2 scaleFactor = GetScaleFactor();
        //Debug.Log(scaleFactor);
        transform.localScale = new Vector3(transform.localScale.x * scaleFactor.x, transform.localScale.y * scaleFactor.x, transform.localScale.z);
        //transform.localPosition = new Vector3((transform.localPosition.x - (transform.localPosition.x*scaleFactor.x)),( transform.localPosition.y-(transform.localPosition.y * scaleFactor.x)), transform.localPosition.z);
        transform.position = new Vector3(transform.position.x * scaleFactor.x, transform.position.y * scaleFactor.x, transform.position.z);
    }

    public void updateMovingObject()
    {

        if (isTouchInsideNode(Input.mousePosition))
        {
            //Debug.Log("touch inside");
            _isTouchInside = true;
        }
        else
        {
            //Debug.Log("touch outside");
            _isTouchInside = false;
        }

        Debug.Log("tuch loc " + Input.mousePosition);
        if (_isTouchInside && Input.GetMouseButton(0))
        {
            _currentTouchPosition = Input.mousePosition; // Update drag origin for smooth dragging
            _currentDirection = calculateDirection(_currentTouchPosition);
            SetBodyPosition(_currentDirection);
            //moveBall(_currentDirection);
        }

    }

    private bool isTouchInsideNode(Vector3 touchLoc)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, touchLoc, null, out localPoint);
        if (localPoint.x >= -(ControlSize.x / 2f) && localPoint.x <= (ControlSize.x / 2f) && localPoint.y >= -(ControlSize.y / 2f) && localPoint.y <= (ControlSize.y / 2f))
        {
            return true;
        }
        return false;
    }

    public Vector3 convertToDesignTouch(Vector3 data)
    {
        Vector2 scaleFactor = GetScaleFactor();
        float designTouchX = data.x * scaleFactor.x;
        float designTouchY = data.y * scaleFactor.y;
        return new Vector3(designTouchX, designTouchY, 0);
    }

    public Vector2 GetScaleFactor()
    {
        float designWidth = Screen.width;
        float designHeight = Screen.height;
        float actualScreenWidth = 1920.0f;
        float actualScreenHeight = 1080.0f;
        float scaleFactorX = designWidth / actualScreenWidth;
        float scaleFactorY = designHeight / actualScreenHeight;
        return new Vector2(scaleFactorX, scaleFactorY);
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
        }
        if (_isMouseDown)
        {
            updateMovingObject();
        }

    }*/
    private IEnumerator StartScheduler()
    {
        while (true)
        {
            // Perform your scheduled task here
            //Debug.Log("Scheduled task executed");
            if (_isMouseDown)
            {
                // TODO update position here
                updateMovingObject();
            }

            // Wait for a specific amount of time before executing the task again
            yield return new WaitForSeconds(0.001f); // Adjust the delay time as needed
        }
    }

    private void StopScheduler()
    {
        if (schedulerCoroutine != null)
        {
            StopCoroutine(schedulerCoroutine);
            schedulerCoroutine = null;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_joystickCenter = Input.mousePosition;
            _isMouseDown = true;
            schedulerCoroutine = StartCoroutine(StartScheduler());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
            //_isNewDirection = false;
            StopScheduler();
        }
        /* if (_isMouseDown)
         {
            // TODO update position here
             _currentTouchPoint = Input.mousePosition;
             UpdateObjectPosition();
         }*/
    }
}
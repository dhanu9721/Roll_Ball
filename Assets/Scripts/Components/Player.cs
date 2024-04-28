using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody _rigidBody;
    public GameObject Health;
    private Transform HealthContainer;
    private Transform MystartPoint;
    //private GameOver _GameOver;
    private List<PlayerHealth> Healths = new List<PlayerHealth>();

    private FixedJoystick _fixedJoystick;

    public PlayerData playerData;
    public Material playerMateral;
    public List<TeleportPoint> TeleportPointsList = new List<TeleportPoint>();

    bool _isPositionInitiallySet = false;

    bool isPlayerTouchedTeleport = false;
    public void Start()
    {
        if(playerData != null) {
            //Co_Ordinates playerPosition = playerData.playerObject.transform.Position;
            //gameObject.transform.position = MystartPoint.position;
        }
    }
    public void init(PlayerData playerdata,Transform startPoint,  List<TeleportPoint> teleportPointsList, FixedJoystick fixedJoystick, Transform healthContainer)
    {
        Debug.Log("init  : "+ playerData.playerObject.materialColor);
        playerData = playerdata;
        MystartPoint = startPoint;
        setTagAndMaterial(gameObject, playerMateral, playerData.playerObject);
        TeleportPointsList = teleportPointsList;
        _fixedJoystick = fixedJoystick;
        HealthContainer = healthContainer;
        gameObject.transform.position = MystartPoint.position;
        //_GameOver = gameOver;
        AddHealth(playerdata.health);
    }


    //private void OnCollisionEnter(Collider other)
        private void OnCollisionEnter(Collision other)
    {
        // Check if the ball collides with the red teleportation point
        //bool isTeleport = checkTeleportTags(other.gameObject.tag);
        bool isTeleport = TeleportColliders.CheckCollider(other.gameObject.tag);
        if (isTeleport)
        {
            // Teleport the ball to the blue teleportation point
            TeleportPoint teleportPointComponent = other.gameObject.GetComponent<TeleportPoint>();
            if(teleportPointComponent.isBallOutSideTeleportPoint) {
                transform.position = teleportPointComponent.SetBallToSiblingPosition().position;
            }
        }

        bool isWall = WallColliders.CheckCollider(other.gameObject.tag);

        if(isWall && !isPlayerTouchedTeleport)
        {
            // TODO : Add Game over here
            EventEmitterClass.GetInstance().VibrateEventEmitter.Emit(EVENT_TYPES.VIBRATE_EFFECT ,"vibrating");
            CheckPlayerHealth();
        } 
        bool isGameOverPoint = GameOverColliders.CheckCollider(other.gameObject.tag);

        if(isGameOverPoint)
        {
            // TODO : Add Game over here
           // _GameOver.gameObject.SetActive(true);
            GameModel.screenManager.showScreen(ScreenEnum.GameWinScreen, playerData.currentLevelData);
            //_GameOver.OnWin();
        }
    }

    public void BallMoveOutSideAnimation(Vector3 currentPosition, Vector3 originalScale)
    {
        transform.position = currentPosition;
        //LeanTween.move(gameObject, targetPosition, 0.5f).setEase(LeanTweenType.linear).setOnComplete(() => { isPlayerTouchedTeleport = false; });
        LeanTween.scale(gameObject, originalScale, 0.3f)
           .setEase(LeanTweenType.easeOutQuad)
           .setOnComplete(() => { isPlayerTouchedTeleport = false; });
    }

    public void BallMoveInsideAnimation(Vector3 currentPosition, Vector3 targetPosition)
    {
        //LeanTween.move(gameObject, currentPosition, 0.5f).setEase(LeanTweenType.linear).setOnComplete(() => BallMoveOutSideAnimation(new Vector3(targetPosition.x, targetPosition.y-10, targetPosition.z -10), targetPosition));
        Vector3 originalScale = transform.localScale;

        // Tween to scale down to 0
        LeanTween.scale(gameObject, Vector3.zero, 0.3f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(()=>BallMoveOutSideAnimation(targetPosition, originalScale));
    }


    public void BallMoveAnimation(Vector3 currentPosition, Vector3 targetPosition)
    {
        isPlayerTouchedTeleport = true;
        LeanTween.move(gameObject, currentPosition, 0.5f).setEase(LeanTweenType.linear).setOnComplete(() => BallMoveInsideAnimation(new Vector3(currentPosition.x, currentPosition.y-10, currentPosition.z-10), targetPosition));
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball collides with the red teleportation point
        bool isTeleport = TeleportColliders.CheckCollider(other.tag);
        if (isTeleport)
        {
            // Teleport the ball to the blue teleportation point
            TeleportPoint teleportPointComponent = other.gameObject.GetComponent<TeleportPoint>();
            if (teleportPointComponent.isBallOutSideTeleportPoint)
            {
                //transform.position = teleportPointComponent.SetBallToSiblingPosition().position;
                BallMoveAnimation(teleportPointComponent.gameObject.transform.position, teleportPointComponent.SetBallToSiblingPosition().position);
            }
        }

        bool isWall = WallColliders.CheckCollider(other.gameObject.tag);

        if (isWall)
        {
            // TODO : Add Game over here
            CheckPlayerHealth();
        }
        bool isGameOverPoint = GameOverColliders.CheckCollider(other.gameObject.tag);

        if (isGameOverPoint)
        {
            // TODO : Add Game over here
            //_GameOver.gameObject.SetActive(true);
            GameModel.screenManager.showScreen(ScreenEnum.GameWinScreen, playerData.currentLevelData);
            //_GameOver.OnWin();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        bool isTeleport = TeleportColliders.CheckCollider(other.tag);
        if (isTeleport)
        {
            // Teleport the ball to the blue teleportation point
            TeleportPoint teleportPointComponent = other.GetComponent<TeleportPoint>();
            teleportPointComponent.isBallOutSideTeleportPoint = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        bool isTeleport = TeleportColliders.CheckCollider(other.gameObject.tag);
        if (isTeleport)
        {
            // Teleport the ball to the blue teleportation point
            TeleportPoint teleportPointComponent = other.gameObject.GetComponent<TeleportPoint>();
            teleportPointComponent.isBallOutSideTeleportPoint = true;
        }
    }

    public void CheckPlayerHealth()
    {
        if(playerData.health <= 1)
        {
            DeactivateHealth();
            Invoke("ShowGameOverScreen", 1f);
            
            //_GameOver.gameObject.SetActive(true);
            //_GameOver.OnLoose();
        }
        else
        {
           DeactivateHealth();
           transform.position =MystartPoint.position;
           playerData.health -= 1;
        }
    }

    public void ShowGameOverScreen()
    {
        GameModel.screenManager.showScreen(ScreenEnum.GameOverScreen, playerData.currentLevelData);
    }

    public void setInitialLocalScale(Vector3 localScale)
    {
        playerData.localScale = localScale;
    }

    /*void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput * playerData.speed * Time.deltaTime, 0f, verticalInput * playerData.speed * Time.deltaTime);

        // Apply the movement to the ball
        transform.Translate(movement);

        transform.localScale = playerData.localScale;
        //transform.rotation = Quaternion.EulerRotation(0f, 0f, 0f);
        transform.localRotation = playerData.localRotation;
    }*/

    private void FixedUpdate()
    {
        if(LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL) == 0 || LocalDataManager.GetPlayerProgressIntType(LOCAL_DATA_KEYS.SELECTED_CONTROL) == (int)ControlTypes.Joystick)
        {
            if (!_isPositionInitiallySet)
            {
                gameObject.transform.position = MystartPoint.position;
                _isPositionInitiallySet = true;
            }
           // _rigidBody.velocity = new Vector3(_fixedJoystick.Horizontal * playerData.speed , _rigidBody.velocity.y, _fixedJoystick.Vertical * playerData.speed);
            Vector3 moveDirection = new Vector3(_fixedJoystick.Horizontal, 0f, _fixedJoystick.Vertical).normalized;

            // Apply force to the Rigidbody based on the movement direction and speed
            /*if (_rigidBody.velocity.magnitude > 0.1f)
            {
                // Calculate the desired angular velocity for rolling in X and Z directions
                Vector3 angularVelocity = new Vector3(-_rigidBody.velocity.z, 0f, _rigidBody.velocity.x).normalized * 2f;

                // Set the angular velocity directly to ensure proper rolling
                _rigidBody.angularVelocity = angularVelocity;
            }*/

            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
                _rigidBody.AddForce(moveDirection, ForceMode.VelocityChange);
               /* if(_rigidBody.velocity.magnitude > 0.1f)
                {
                    // Calculate the desired angular velocity for rolling in X and Z directions
                    Vector3 angularVelocity = new Vector3(_rigidBody.velocity.z, 0f, -_rigidBody.velocity.x).normalized;

                    // Set the angular velocity directly to ensure proper rolling
                    _rigidBody.angularVelocity = angularVelocity;
                }*/
            }

            //_rigidBody.AddForce(movement, ForceMode.VelocityChange);
            //transform.localScale = playerData.localScale;
            //transform.rotation = Quaternion.EulerRotation(0f, 0f, 0f);
            //transform.localRotation = playerData.localRotation;
            if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
            {
               //transform.rotation = Quaternion.LookRotation(_rigidBody.velocity);
            }
        }
    }

    public void SetTransformOfObject(GameObject instanceObject, Transform objectContainer, RoomEntityObjectTransform objectTransform)
    {
        //var position = objectTransform.Position;
        //var scale = objectTransform.Scale;
        //playerData.playerObject.transform = objectTransform;
        instanceObject.transform.parent = objectContainer;
        //instanceObject.transform.position = new Vector3(position.x, position.y, position.z); // Set position to match the spawn node
        //instanceObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // Example scaling
        //instanceObject.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z); // Example rotation
    }

    public void setTagAndMaterial(GameObject instanceObject, Material objectMaterial, PlayerObject objectData)
    {
        if (objectData.tag != null)
        {
            instanceObject.tag = objectData.tag;
        }
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

    // Start is called before the first frame updat

    public void AddHealth(int healthCount)
    {
        for (int i = 0; i < healthCount; i++)
        {
            GameObject instance = Instantiate(Health, HealthContainer);
            //instance.transform.parent = HealthContainer;
            instance.transform.SetParent(HealthContainer, worldPositionStays: false);
            PlayerHealth healthComponent = instance.GetComponent<PlayerHealth>();
            Healths.Add(healthComponent);
        }
    }
    public void ActivateHealth()
    {
        foreach (var item in Healths)
        {
            if (!item.isHealthActive())
            {
                item.ActivateHealth();
                break;
            }
        }
    }

    public void DeactivateHealth()
    {
        if(playerData.health > 0)
        {
            foreach (var item in Healths)
            {
                if (item.isHealthActive())
                {
                    item.DeactivateHealth();
                    break;
                }
            }
        }
    }

}


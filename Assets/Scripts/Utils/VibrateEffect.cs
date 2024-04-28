using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateEffect : MonoBehaviour
{

    // Reference to the vibration duration
    public float vibrationDuration = 0.5f;

    // Reference to the vibration intensity
    public float vibrationIntensity = 0.5f;

    // Reference to the camera to shake
    public Camera mainCamera;
    public Camera BGCamera;

    // Flag to check if vibration effect is already playing
    private bool isVibrating = false;

    private Vector3 originalPosition = new Vector3();
    // Start is called before the first frame update

    private void Awake()
    {
        originalPosition = mainCamera.transform.position;
    }

    void Start()
    {
        Action<object[]> listener = (args) => {
            Debug.Log("on start vibrate");
            StartVibrateEffect();
        };
        EventEmitterClass.GetInstance().VibrateEventEmitter.On(EVENT_TYPES.VIBRATE_EFFECT, listener);
    }


    public void ResetCameras()
    {
        mainCamera.transform.position = originalPosition;
    }
    public void StartVibrateEffect()
    {
        // Check if collision involves the ball and a wall
     
            TriggerMobileVibration();

            // Trigger visual vibration effect on the screen
            StartCoroutine(VisualVibrationEffect());
    }

    void TriggerMobileVibration()
    {
        // Check if the device supports vibration
        if (SystemInfo.supportsVibration)
        {
            // Vibrate for a specified duration
            Handheld.Vibrate();
        }
    }

    IEnumerator VisualVibrationEffect()
    {
        // Set the flag to prevent multiple vibrations at the same time
        isVibrating = true;

        // Store the original camera position
       
        //Vector3 BGoriginalPosition = BGCamera.transform.position;

        float elapsed = 0.0f;

        while (elapsed < vibrationDuration)
        {
            // Generate a random offset within a range
            float offsetX = UnityEngine.Random.Range(-vibrationIntensity, vibrationIntensity);
            float offsetZ = UnityEngine.Random.Range(-vibrationIntensity, vibrationIntensity);

            // Apply the offset to the camera position
            mainCamera.transform.position = originalPosition + new Vector3(offsetX,0, offsetZ);
            //BGCamera.transform.position = BGoriginalPosition + new Vector3(offsetX, BGCamera.transform.position.y, offsetY);

            // Increment elapsed time
            elapsed += 0.01f;

            // Wait for the next frame
            yield return null;
        }

        // Reset camera position to the original position
        mainCamera.transform.position = originalPosition;
        //BGCamera.transform.position = BGoriginalPosition;

        // Reset the flag
        isVibrating = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

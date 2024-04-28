using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody _rigidBody;
    public float speed = 30f;
    public float boundaryReflectionForce = 1f;
    private Vector3 reflectionDirection = new Vector3(1f, 0f, 1f);

    void Start()
    {
       
        RotateBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag(ColliderType.Ground))
        {
            Debug.Log("on trigger Ground");
            _rigidBody.useGravity = false;
        }
        if (other != null && other.CompareTag(ColliderType.Boundary))
        {
            // TODO Reflect back
            {
                // Calculate the reflection direction
                //Vector3 reflectionDirection = Vector3.Reflect(_rigidBody.velocity, other.transform.forward);
                // Apply a force in the reflection direction to reflect the ball
                //_rigidBody.AddForce(reflectionDirection * boundaryReflectionForce, ForceMode.Impulse);
                //Vector3 reflectionDirection = Vector3.Reflect(transform.right, other.transform.forward);

                // Apply a force in the reflection direction to reflect the ball
                //_rigidBody.AddForce(reflectionDirection * boundaryReflectionForce, ForceMode.VelocityChange);
                //Vector3 wallNormal = other.transform.forward;
                ChooseDirection();
                //Debug.Log("on trigger Boundary : "+ reflectionDirection);
                //reflectionDirection = Vector3.Reflect(reflectionDirection, other.transform.right);

                // Rotate the ball to face the reflection direction
                //transform.rotation = Quaternion.LookRotation(reflectionDirection);

                // Update the forward direction of the ball to the reflection direction
                //transform.forward = reflectionDirection;
            }
        }
    }

    public void ChooseDirection()
    {
        if(reflectionDirection.x >0f && reflectionDirection.z > 0f)
        {
            reflectionDirection = new Vector3(-1, 0, 1);
        } 
        else if(reflectionDirection.x < 0f && reflectionDirection.z > 0f)
        {
            reflectionDirection = new Vector3(-1, 0, -1);
        }
        else if(reflectionDirection.x < 0f && reflectionDirection.z < 0f)
        {
            reflectionDirection = new Vector3(1, 0, -1);
        }
        else if(reflectionDirection.x > 0f && reflectionDirection.z < 0f)
        {
            reflectionDirection = new Vector3(1, 0, 1);
        }
        
    }

   /* void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with the boundaries
        if (collision.gameObject.CompareTag(ColliderType.Boundary))
        {
            // Calculate the reflection direction
            Vector3 reflectionDirection = Vector3.Reflect(_rigidBody.velocity, collision.contacts[0].normal);

            // Apply a force in the reflection direction to reflect the ball
            _rigidBody.AddForce(reflectionDirection * boundaryReflectionForce, ForceMode.Impulse);
        }
    }*/

    void FixedUpdate()
    {
        // Move the ball continuously in its current direction
        //_rigidBody.AddForce(transform.right * speed, ForceMode.VelocityChange);

        Vector3 movement = reflectionDirection.normalized * speed * Time.fixedDeltaTime;

        // Apply movement to the ball
        transform.Translate(movement, Space.World);
        //transform.Translate(reflectionDirection, Space.World);
    }

    void RotateBall()
    {
        // Rotate the ball continuously around the Z-axis using LeanTween
        LeanTween.rotate(gameObject, new Vector3(360f, 360f, 360f), 1f / (speed/5)).setLoopClamp();
    }

    Vector3 CalculateReflectionDirection(Collider other)
    {
        // Get the collision point using raycasting
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Calculate the reflection direction based on the collision normal
            return Vector3.Reflect(transform.forward, hit.normal).normalized;
        }

        // If raycast fails, return the current forward direction
        return transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);

        // Ensure the ball stays on the y-axis
        transform.position = new Vector3(transform.position.x, 5.5f, transform.position.z);*/
    }
}

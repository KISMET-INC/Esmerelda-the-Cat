using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//****************************************************************
// CAMERA SCRIPT CLASS                                          
// Moves the camera downward                            
//****************************************************************
public class CameraScript : MonoBehaviour
{
        //DECLARE VARIABLES

        //Speed camera falls
    private float speed = 1f;

        //Acceleration factor
    private float acceleration = 0.2f;

    [SerializeField]
    private float maxSpeed = 10f;

        //Flag for moving camera in Start()
    [HideInInspector]
    public bool moveCamera;

    //****************************************************************
    // Awake()
    // Called before first frame renders. Calls MakeInstance()
    // function
    //****************************************************************
    private void Awake()
    {
        MakeInstance();
    }

    //****************************************************************
    // Start()
    // Called when first frame renders. Sets moveCamera bool to 
    // to true.
    //****************************************************************
    void Start()
    {
        moveCamera = true;
    }

    //****************************************************************
    // Update()
    // Calls MoveCamera() function every frame while moveCamera
    // bool is set to true
    //****************************************************************
    void Update()
    {
        if (moveCamera)
        {
            MoveCamera();
        }
    }

    //****************************************************************
    // MoveCamera()
    // Causes the camera to fall. If speed is greater than
    // maxSpeed. Speed resets to maxSpeed
    //****************************************************************
    void MoveCamera()
    {
            //Get the current Vector posistion of the linked camera object
        Vector3 translateCameraDown = transform.position;

            //Get the Y value
        float oldY = translateCameraDown.y;

            //Get the Y value and subtract the speed of the camera times the seconds since the last frame (Time.deltatime)
        float newY = translateCameraDown.y - (speed * Time.deltaTime);

            //Create a transition by clamping the the translate value between the last location and the projected
            // new location using the speed variable
        translateCameraDown.y = Mathf.Clamp(translateCameraDown.y, oldY, newY);

            //Move the camera to the clamped variable above
        transform.position = translateCameraDown;

            //Introduce the acceleration
        speed += acceleration * Time.deltaTime;

            //Max out the speed
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    //****************************************************************
    // SetCameraSpeed()
    // Used by GameManager to set a higher cameraSpeed for next
    // life
    //****************************************************************
    public void SetCameraSpeed(float newSpeed)
    {
            //If the newSpeed is decreased by two but will be less than the initial camera speed
            //of 1, the camera speed is set back to the intitial camera speed of 1
        if (newSpeed - 2 < 1)
        {
            speed = 1f;
        }

            //If losing 2 points wont make the speed to low, proceed in reducing the cameara speed.
        else
        {
            speed = newSpeed - 2f;
        }
    }

    //****************************************************************
    // GetCameraSpeed()
    // Returns the current speed of the camera
    //****************************************************************
    public float GetCameraSpeed()
    {
        return speed;
    }


    //****************************************************************
    // BEGIN CREATE INSTANCE
    //**************************************************************** 
    public static CameraScript instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // END CREATE INSTANCE


} // EMD CAMERA SCRIPT
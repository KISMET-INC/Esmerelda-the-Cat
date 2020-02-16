using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//****************************************************************
// JOYSTICK SCRIPTS CLASS
// Controlls the left and right screen buttons
//****************************************************************
public class JoystickScripts : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
        //DECLARE VARIABLES
        //Link a joystick controller
    private PlayerJoystickScript playerMove;

    //****************************************************************
    // OnPointerDown()
    // When a button press event is registered, determine if it is
    // the left button. If yes, call SetMoveLeft() function and
    // set to true, else call SetMoveLeft() and set to false (aka) 
    // equivalent to SetMoveRight.
    //****************************************************************
     public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "Left Button") {
            playerMove.SetMoveLeft(true);
            Debug.Log("down left");
        }
        else
        {
            playerMove.SetMoveLeft(false);
            Debug.Log("down right");
        }
    }

    //****************************************************************
    // OnPointerUp()
    // If no button is registered as being pressed, stop moving
    // the player
    //****************************************************************
    public void OnPointerUp(PointerEventData data)
    {
        Debug.Log("up");
        playerMove.StopMoving();
       
    }

    //****************************************************************
    // Start()
    // Link the joystick controller to playerMove variable
    //****************************************************************
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerJoystickScript>();
    }

} // END JOYSTICK SCRIPTS

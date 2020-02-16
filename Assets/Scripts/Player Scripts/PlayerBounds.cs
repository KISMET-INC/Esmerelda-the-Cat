using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//****************************************************************
// PLAYER BOUNDS CLASS
// Sets the bounds so the player does not fly off the screen
//****************************************************************

public class PlayerBounds : MonoBehaviour
{

 
    //Min and max player bounds;
    private float minX, maxX;

    //****************************************************************
    // Start()                                                      
    // Loads with first frame. Call SetMinAndMax function.          
    //****************************************************************
    void Start()
    {
        SetMinAndMax();
    }

    //****************************************************************
    // Update()                                                     
    // Called every frame. If players position is found to be off   
    // screen (less than min or greater than max) reposition the    
    // player back at the edge.                                     
    //****************************************************************
    void Update()
    {
        // Off screen left
      if (transform.position.x < minX)
        {
            Vector3 transformPlayer = transform.position;
            transformPlayer.x = minX;
            transform.position = transformPlayer;
            Debug.Log("Min bound");            
        }
        // Off screen right

        if (transform.position.x > maxX)
        {
            Vector3 transformPlayer = transform.position;
            transformPlayer.x = maxX;
            transform.position = transformPlayer;
            Debug.Log("Max bound");
        }

    }

    //****************************************************************
    // SetMinAndMax                                                 
    // Use the camera to find the bounds of the screen then set    
    // the positive and negative values as teh min and max bounds   
    // for the player.                                              
    //****************************************************************
    void SetMinAndMax()
    {
        //Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxX = 6f;
        minX = -6f;
    }
} // END PLAYER BOUNDS CLASS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************
// BG SCALAR CLASS                                              
// Scales background image to screen/camera size.               
//****************************************************************
public class BGScalar : MonoBehaviour
    {
       
        void Start()
        {
                //Locate the sprite for scaling
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

                //Create a vector that holds the scale value of the object the script is linked to
            Vector3 tempScale = transform.localScale;

                //Convert the x value of the size of the sprite into a float
            float width = sr.sprite.bounds.size.x;
        
                //Get the world height and width from the camera
            float worldHeight = Camera.main.orthographicSize * 2f;
            float worldWidth = worldHeight / Screen.height * Screen.width;

                //Assign the x value of the vector to the world width divided by the width of the sprite
              tempScale.x = worldWidth / width;

                //Resize the scripts object to the vector created and manipulated above.
            transform.localScale = tempScale;
        }
    } // END BG SCALAR


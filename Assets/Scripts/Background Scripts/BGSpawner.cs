using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//****************************************************************
// BG SPAWNER CLASS                                                          
// Spawns new backgrounds when last background is set           
// inactive.                                                    
//****************************************************************
public class BGSpawner : MonoBehaviour
{
        //Array to hold background game objects
    private GameObject[] backgrounds;

        //variable for the location of the last background game object
    private float lastY;

    //****************************************************************
    // Start()                                                      
    // Call function GetBackgroundsAndSetLastY()                    
    //****************************************************************
    void Start()
    {
        GetBackgroundsAndSetLastY();    
    }


    //****************************************************************
    // GetBackgroundAndSEtLastY()                                   
    // Iterate through the backgrounds array until the last         
    // background is found and set its Y value to lastY variable.   
    //****************************************************************
    void GetBackgroundsAndSetLastY()
    {
            //Collect all the game objects tagged "background" into the backgrounds array
        backgrounds = GameObject.FindGameObjectsWithTag("background");

            //Assign the first element in the backgrounds array as the potential Y location of the last background
        lastY = backgrounds[0].transform.position.y;

            //Iterate through the array comparing the Y locations of every background until the Y location 
            //of the last background object has been found and set this value as the lastY value;
        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (lastY > backgrounds[i].transform.position.y)
               lastY = backgrounds[i].transform.position.y;           
        }
    }

    //****************************************************************
    // OnTriggerEnter2D()                                           
    // Respawn background if it is not active                       
    //****************************************************************
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "background")
        {
                //if the colliding background object has the same value as the value of the object determined as the last 
            if (target.transform.position.y == lastY)
            {
                    //Create a vector holding the position of the colliding object
                Vector3 temp = target.transform.position;

                    //Get the Y height of the target
                float height = ((BoxCollider2D)target).size.y;

                    //Iterate through the backgrounds array
                for (int i = 0; i < backgrounds.Length; i++)
                {
                        //for every background not active create a new spawning Y value by subtracting 
                        //the height of the object, thus spawning new object directly beneath the last.
                    if (!backgrounds[i].activeInHierarchy)
                    {
                            //use temp.y variable to place the spawning branch beneath the lats
                        temp.y -= height;

                            //Make lastY equal to the value of the one before
                        lastY = temp.y;

                            //Position current background using modified variable above.
                        backgrounds[i].transform.position = temp;

                            //Set current background to active
                        backgrounds[i].SetActive(true);
                    }
                }
            }
        }
    }
}

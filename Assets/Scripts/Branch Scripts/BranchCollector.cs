using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//****************************************************************
// BRANCH COLLECTOR CLASS
// Deacitvates branches on collision
//****************************************************************
public class BranchCollector : MonoBehaviour
{

    //****************************************************************
    // OnTrigger(Branch Collector)                                  
    // If any object with a specific tag collides with the Branch   
    // Collector object then the colliding object will be set to    
    // inactive so it can be respawned later.                       
    //****************************************************************
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "goodBranch" || target.tag == "badBranch" || target.tag =="treat" || target.tag == "life")
        {
            target.gameObject.SetActive(false);
        }     
    }
}

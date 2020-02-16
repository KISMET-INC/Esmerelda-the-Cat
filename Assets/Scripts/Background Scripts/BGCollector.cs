using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //****************************************************************
    // BG COLLECTOR CLASS
    // Deactivates backgrounds
    //****************************************************************
public class BGCollector : MonoBehaviour
{
    //****************************************************************
    // OnTriggerEnter2D()                                           
    // If the collider hits a target and that target has the tag    
    // "background" set target object inactive                      
    //****************************************************************
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "background")
        {
            target.gameObject.SetActive(false);
        }
    }
}

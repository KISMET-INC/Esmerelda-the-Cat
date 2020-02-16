using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    void destroyCollectable()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
       // Invoke("destroyCollectable", 20f);
        
    }

   
}

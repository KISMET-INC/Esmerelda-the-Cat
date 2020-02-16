using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************
// MUSIC CONTROLLER CLASS
// Manages the game music and sound effects
//****************************************************************
public class MusicController : MonoBehaviour
{
   //DECLARE VARIABLES

    private AudioSource audioSource;

    //****************************************************************
    // Awake()
    // Call MakeSingleton() and link audio component for
    // manipulation within class
    //***************************************************************
    void Awake()
    {
        MakeSingleton();
        audioSource = GetComponent<AudioSource>();
    }

    //****************************************************************
    // PlayMusic()
    // Stop and start main audiosource determined by bool
    //****************************************************************
    public void PlayMusic(bool play)
    {       
            if (play)
                {
                    audioSource.Play();
                }
                                             
            else if (!play)
                {
                    audioSource.Stop();
                }
            }

    //****************************************************************
    // MAKE SINGLETON INSTANCE
    // Allows the music to be monitored from all scenes of the
    // game
    //****************************************************************
    public static MusicController instance;
    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


} // END MUSIC CONTROLLER

   


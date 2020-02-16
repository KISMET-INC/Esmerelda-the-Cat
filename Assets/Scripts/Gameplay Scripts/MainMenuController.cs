using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//****************************************************************
// MAIN MENU CONTROLLER CLASS
// Orchastrates the user interface for the main menu scene.
//****************************************************************
public class MainMenuController : MonoBehaviour
{
     // DECLARE VARIABLES

    public bool gameStartedFromMainMenu;
    bool MusicIsPlaying;

    [SerializeField]
    private Button musicButton;

        //Array to hold varying image states of music button (on/off)
    [SerializeField]
    private Sprite[] musicIcons;

  
    //****************************************************************
    // CheckToPlayTheMusic2()
    // Check the game prefs settings and whether the music is
    // already playing to determine what the music should be doing
    // when the game starts
    //****************************************************************
    void CheckToPlayTheMusic2()
    {
            //Flag to tell if music is currently playing
        bool MusicIsPlaying = MusicController.instance.GetComponent<AudioSource>().isPlaying;
       
        if (MusicIsPlaying) //at startup
        {
                //Match the icon
            musicButton.image.sprite = musicIcons[0];
        }
            //if music isnt already playing, check game prefs
            //if game prefs says music should be on, turn on music and set icon to match
        else if (GamePreferences.getMusicState() == 1)
        {
            MusicController.instance.PlayMusic(true);
            musicButton.image.sprite = musicIcons[0];
        }
  
            //if game prefs says music should be off, turn off music and set icon to match
        else if (GamePreferences.getMusicState() == 0)
        {
            MusicController.instance.PlayMusic(false);
            musicButton.image.sprite = musicIcons[1];
        }
    }

    //****************************************************************
    // Awake()
    // Calls MakeInstance() function
    //****************************************************************
    private void Awake()
    {     
        MakeInstance();       
    }

    //****************************************************************
    // Start()
    // Call Game Manager to read in the high score from the file and
    // save it so it is ready to reference throughout the game.
    //****************************************************************
    void Start()
    {
        CheckToPlayTheMusic2();
    }

    //****************************************************************
    // StartGame()
    // Referenced directly through Unity Editor OnClick().
    // Loads the Gameplay Scene and sets the flag for 
    // gameStartedFromMainMenu to true.
    //****************************************************************
    public void StartGame()
    {      
        GameManager.instance.gameStartedFromMainMenu = true;

        StartCoroutine(SceneFader.instance.LoadLevel("GamePlay"));
    }

 
    //****************************************************************
    // HighScoreMenu()
    // Loads High score scene. Referenced from Unity Editor
    //****************************************************************
    public void HighscoreMenu()
    {
        StartCoroutine(SceneFader.instance.LoadLevel("HighScore"));
    }

    //****************************************************************
    // OptionsMenu()
    // Load the options scene. Referenced from Unity Editor
    //****************************************************************
    public void OptionsMenu()
    {
        StartCoroutine(SceneFader.instance.LoadLevel("Options"));
    }

    //****************************************************************
    // QuitGame()
    // Quits the game. Referenced from Unity Editor
    //****************************************************************
    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }

    //****************************************************************
    // MusicButton()
    // What happens when the user clicks the music button
    //****************************************************************
    public void MusicButton()
    {
            //Flag to check if music is playing
        bool MusicIsPlaying = MusicController.instance.GetComponent<AudioSource>().isPlaying;

            //CHECK GAME PREFS FOR CURRENT MUSIC STATE

            //If the music pref is off and the user presses the button, turn it on and set the icon to match
            //set the new music state for later use
        if (GamePreferences.getMusicState() == 0) //off
        {
            MusicController.instance.PlayMusic(true);
            musicButton.image.sprite = musicIcons[0];
            GamePreferences.setMusicState(1);         
        }

            //If the music pref is on, and the music is NOT playing,
            //and the user presses the button, turn it off and set the icon to match
            //set the new music state for later use
        else if (GamePreferences.getMusicState() == 1 && !MusicIsPlaying) //on
        {
            MusicController.instance.PlayMusic(true);
            musicButton.image.sprite = musicIcons[0];
            GamePreferences.setMusicState(0);       
        }

            //If the music pref is on, and the music IS playing,
            //and the user presses the button, turn it off and set the icon to match
            //set the new music state for later use
        else if (GamePreferences.getMusicState() == 1 && MusicIsPlaying)
        {
            musicButton.image.sprite = musicIcons[1];
            MusicController.instance.PlayMusic(false);
            GamePreferences.setMusicState(0);
        }
    }

    //****************************************************************
    //BEGIN CREATE INSTANCE
    //****************************************************************
    public static MainMenuController instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //END CREATE INSTANCE

} // END MAIN MENU CONTROLLER

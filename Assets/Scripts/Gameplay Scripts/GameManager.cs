using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//****************************************************************
// GAME MANAGER CLASS
// Moves through all of the scenes and is the record keeper
// for score, treats, and lives. Also determines what starting
// values are depending on whether game was an initial start
// or a reload after a death. Reads and writes to a highscore file
// so that values remain after game closes.
//****************************************************************
public class GameManager : MonoBehaviour {

        //DECLARE VARIABLES
   
    [SerializeField]
    private List<int> highScoreList = new List<int>();
    
        //Two flag variables to help tell the GameManager how to record and show the score and life data
    [HideInInspector]
    public bool gameStartedFromMainMenu;
    [HideInInspector]
    public bool gameRestartedAfterPlayerDied;

        //Collectable scores - public variable so that other scripts and scenes can record data to the GameManager/RecordKeeper
    [HideInInspector]
    public int score = 0, treatScore = 0, lifeScore = 0;
 
        //Change the camera speed depending on if player is restarting or has died
        //Restarting game resets gamespeed, Respawning sets camera speed at a fraction of speed player died 
    [HideInInspector]
    public float newCameraSpeed = 1f;
    [HideInInspector]
    public float initialCameraSpeed = 1f;

    //****************************************************************
    // Awake()
    // MakeSingleton() and InitializeVariables()
    //****************************************************************
    void Awake()
    {
        makeSingleton();
        InitializeVariables();
    }

    //****************************************************************
    // Start()
    //
    //****************************************************************
    private void Start()
    {
       
    }

    //****************************************************************
    // SCENE MANAGER FUNCTIONS
    // To test if "Gameplay" scene is loaded and how to proceed
    // in various situations
    //****************************************************************
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    //****************************************************************
    // LevelLoaded()                                                
    // Sets starting conditions for players score,lifec count, and  
    // camear speed. Also sets respawn conditions players score     
    // and lifecount and cameraspeed                                
    //****************************************************************
    void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay")
        {
                // Fresh start conditions
            if (gameStartedFromMainMenu == true)
            {
                score = 0;
                treatScore = 0;
                lifeScore = 9;
              
                CameraScript.instance.SetCameraSpeed(initialCameraSpeed);

                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetLifeScore(lifeScore);
                GameplayController.instance.SetTreatScore(treatScore);

            }
            else
            {
                    // Respawn conditions, carrying score and life data from when player died
                CameraScript.instance.SetCameraSpeed(newCameraSpeed);

                GameplayController.instance.SetScore(score);
                GameplayController.instance.SetLifeScore(lifeScore);
                GameplayController.instance.SetTreatScore(treatScore);
            }
        }
    }
    //LEVEL LOADED and SCENE MANAGER END

    //****************************************************************
    // SaveHighScoresList()
    // Saves the high scores list to an encrypted file
    //****************************************************************
    public void SaveHighScoresList(List<int> highScoreList)
    {
            //Create a local high score list
         List<int> list = highScoreList;

            //Formatter to encrypt the file as binary
        BinaryFormatter formatter = new BinaryFormatter();

            //Create a path and filename
        string path = Application.persistentDataPath + "/highscore.txt";

            //Open the filestream, pas the path, and direct it to open or create
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            //Easy way to convert numbers to a string using an empty tempstring
        string tempString = "";

            //Iterate through and add the highscores to the string
            //seperated by '/'  (34/553/343)
       
        for (int i = 0;  i < list.Count; i++)
          {           
            tempString = tempString + list[i].ToString()+"/";            
          }

            //Save the string as a file 
        formatter.Serialize(stream, tempString);
        stream.Close();
    }

    //****************************************************************
    // LoadHighScoresList()
    // Saves the high scores list to an encrypted file
    //****************************************************************
    public List<int> LoadHighScoresList()
    {
            //Create a local list to save the file to
        List<int> highScoresList = new List<int>();
            //The get path
        string path = Application.persistentDataPath + "/highscore.txt";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

                //Stream and decode the file and save as a string
            string highScores = formatter.Deserialize(stream) as string;

                //Create a string array that takes the previous string
                //and splits the elements where there are /
            string[] highScoresArray = highScores.Split('/');

                //Iterate through and add each element to the list
            for (int i = 0; i < highScoresArray.Length-1; i++)
            {
                //Change the strings to ints and add them to the highscoreList
                highScoresList.Add(int.Parse(highScoresArray[i]));
             }

            stream.Close();

            return highScoresList;
        }
        else
        {
                // If the file does not exist, return an empty list
            return highScoresList;
        }
    }

    //****************************************************************
    // InitializeVariables()
    // Set starting conditions for level difficulty and music state
    //****************************************************************
    void InitializeVariables()
    {
            //If there is no "Game Initialized" key
        if (!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.setEasyDifficulty(0);
            GamePreferences.setMediumDifficulty(1);
            GamePreferences.setHardDifficulty(0);

            GamePreferences.setMusicState(1);
         
                //Set a key to Game Intiialized so it will not
                //initialize again
            PlayerPrefs.SetInt("Game Initialized", 1);
        }
    }

    //****************************************************************
    // CREATE SINGLETON INSTANCE OF CLASS
    // Create an singleton instance of the class for easy 
    // transfer of resources between classes
    //****************************************************************
    public static GameManager instance;

    //Use singleton so that this class can  carry data between scenes.
    void makeSingleton()
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


}// END GAME MANAGER


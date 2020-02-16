using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//****************************************************************
// PLAYER SCORE CLASS
// Tracks player score and determines what player has collided
// with
//****************************************************************
public class PlayerScore : MonoBehaviour
{
        //DECLARE VARIABLES

    public static int scoreCount;
    public static int lifeCount;
    public static int treatCount;

    public static int highScore;

        //Collectable Sound Effects
    [SerializeField]
    private AudioClip treatSound, lifeSound, failedSound;
    private float volume = 1f;

        //Call instance of CameraScript to stop camera motion on player death
    [SerializeField]
    private CameraScript cameraScript;

        //Reference the New High Score Text box to alert when player has beaten
        //last high score
    private GameObject NewHighScoreText;

        //Flag to turn off and on score counting which is related to falling
    private bool countScore;

    private Vector3 previousPosition;
   
    //****************************************************************
    // Start()
    // Prepare the scene and all the objects and variables for
    // gameplay
    //************************************************************
    void Start()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();

            //Activate the player object
        gameObject.SetActive(true);

            //Get the players position
        previousPosition = transform.position;
        
            //Start tracking score
        countScore = true;

            //Get starting variables from the Game Manager
        scoreCount = GameManager.instance.score;
        lifeCount = GameManager.instance.lifeScore;
        treatCount = GameManager.instance.treatScore;
    }

    //****************************************************************
    // CountScore()
    // Calculate player score based on movement downward
    //**************************************************************
    void CountScore()
    {
        if (countScore)
        {
            //If players new position is less than the previous position
            //score goes up
        if (transform.position.y < previousPosition.y)
            {
                scoreCount++;
            }
            previousPosition = transform.position;
            GameplayController.instance.SetScore(scoreCount);
        }
    }


    //****************************************************************
    // Update()
    // Calls CountScore() function. Calculates the score every
    // frame
    //****************************************************************
    void Update()
    {
        CountScore();        
    }

    //****************************************************************
    // OnTriggerEnter2D()
    // Determines what happens when the player collides with
    // treats, lives, and dead branches.
    //****************************************************************
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "treat")
        {
            treatCount++;
            scoreCount += 200;

            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetTreatScore(treatCount);

                //Play clip, move audio source close to the camera for added volume
            if (GamePreferences.getMusicState() == 1)
                AudioSource.PlayClipAtPoint(treatSound, .9f*Camera.main.transform.position + 0.09f*transform.position, volume);

            target.gameObject.SetActive(false);
        }

        if (target.tag == "life")
        {
            lifeCount++;
            scoreCount += 300;

            GameplayController.instance.SetScore(scoreCount);
            GameplayController.instance.SetLifeScore(lifeCount);

                //Play clip, move audio source close to the camera for added volume
            if (GamePreferences.getMusicState() == 1)
                AudioSource.PlayClipAtPoint(lifeSound, 0.9f * Camera.main.transform.position + 0.09f * transform.position, volume);

            target.gameObject.SetActive(false);
        }

        // DETERMINE IF FINAL DEATH OR JUST RELOAD
        if (target.tag == "bounds")
        {
                //END OF GAME
            if (lifeCount <= 1)
            {
                    //Deactivate object, stop camera, stop counting score, show life reduction in gameplay textbox
                gameObject.SetActive(false);
                cameraScript.moveCamera = false;
                countScore = false;
                lifeCount=0;

                MusicController.instance.GetComponent<AudioSource>().Pause();

                if (GamePreferences.getMusicState() == 1)
                    AudioSource.PlayClipAtPoint(failedSound, 0.9f * Camera.main.transform.position + 0.09f * transform.position, volume);

                    //Update high scores list
                DetermineHigestScore2(scoreCount);

                    //Use Gameplay controller to show appropriate UI elements and panels for gameplay
                GameplayController.instance.SetLifeScore(lifeCount);
                GameplayController.instance.GameOverShowScorePlayerDied(scoreCount, treatCount);
            }
            else
            {
                    // END OF ROUND
                    //Deactivate player, stop camera, stop counting score, reduce lives by 1
                gameObject.SetActive(false);
                cameraScript.moveCamera = false;
                countScore = false;
                lifeCount--;

                    //Get the cameraSpeed upon player death
                float cameraSpeed = CameraScript.instance.GetCameraSpeed();
                    //Use data recoreded in game manager to set new camera speed so at higher levels
                    // player doesnt have to start off at beginning speed
                GameManager.instance.newCameraSpeed = cameraSpeed;

                    //Play failed sound and position sound close to camera for maximum volume
                if (GamePreferences.getMusicState() == 1)
                    AudioSource.PlayClipAtPoint(failedSound, 0.9f * Camera.main.transform.position + 0.09f * transform.position, volume);

                    //Use gameplay controller to set approrpiate text boxes
                GameplayController.instance.GameOverShowScore(scoreCount, treatCount);
                GameplayController.instance.SetLifeScore(lifeCount);

                    // Let the game manager know this is only the end of a round not a resatart, thus maintaining
                    //current life, treat, and score for the enxt round
                GameManager.instance.gameStartedFromMainMenu = false;
                
                    //Log the score variables with the game manager
                GameManager.instance.score = scoreCount;
                GameManager.instance.lifeScore = lifeCount;
                GameManager.instance.treatScore = treatCount;
            }
        }

        
        if (target.tag == "badBranch")
        {
         
            if (lifeCount <= 1)
            {
                    //Causes the dead branch to fall
                target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                lifeCount = 0;
                scoreCount -= 100;

                    //branch wont make your life go below 1 or your score go below 0
                if (lifeCount < 0)
                    lifeCount = 0;
                if (scoreCount < 0)
                    scoreCount = 0;
             
                    //Shows the new life count in text box.
                GameplayController.instance.SetLifeScore(lifeCount);

            }
            //END OF ROUND
            else
            {
                    //Cause branch to fall
                target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                
                    //Reduce life and score
                lifeCount--;
                scoreCount -= 100;

                    //Set min values
                if (lifeCount <= 0)
                    lifeCount = 0;
                if (scoreCount < 0)
                    scoreCount = 0;
                
                    //Show the new life count in gameplay textbox
                GameplayController.instance.SetLifeScore(lifeCount);

                    //Log the new life count with the game manager
                GameManager.instance.lifeScore = lifeCount;
            }
        }
    }

    //****************************************************************
    // DetermineHighestScor2()
    // Upon final death determine if this score is a new high
    // score, if so display the high score alert in the score
    // panel
    //****************************************************************
    public void DetermineHigestScore2(int score)
    {   
            //Import list from file into a local ist
        List<int> highScoreList = GameManager.instance.LoadHighScoresList();
            
            //If the score is not currently on the list add it to the list
            //then sort the list in ascending order
        if (!highScoreList.Contains(score)){
            highScoreList.Add(score);
            highScoreList.Sort();

                //if the list has more than 10 elements delete the lowest one
            if (highScoreList.Count > 10)
            {
                highScoreList.RemoveAt(0);
            }
                //Save the new list to a file
            GameManager.instance.SaveHighScoresList(highScoreList);
        }
    }
}// END PLAYERSCORE

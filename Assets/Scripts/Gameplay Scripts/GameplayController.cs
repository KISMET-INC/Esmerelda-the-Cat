using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//****************************************************************
// GAMEPLAY CONTROLLER CLASS
// Ochastrates the user interface events invovled in gameplay.
// Including the text boxes for treats, score, lives.
//****************************************************************
public class GameplayController : MonoBehaviour
{


    //Flag to signal player is ready to start
    public bool ready;

    private int highScore;

    //Editable text fields, serialized so that visual game objects can be linked to them
    [SerializeField]
    private Text scoreText, treatText, lifeText, gameOverScoreText, gameOverTreatText, newHighScoreAlertText;

    //Panals serialized to set graphics to them
    [SerializeField]
    private GameObject pausePanel, gameOverPanel, readyButton;


    //****************************************************************
    // Awake()
    // Runs before first frame is rendered. Make instance and set
    // time scale to 0.
    //****************************************************************
    void Awake()
    {
        MakeInstance();      
        
    }

    //****************************************************************
    // Start()
    // Game defaults to ready = false.
    //****************************************************************
    private void Start()
    {      
        ready = false;
        Time.timeScale = 0f;
    }

    //****************************************************************
    // ReadyToStart()
    // Referenced directly through the Unity Editor OnClick().
    // Deactivates the ready image and starts the time scale.
    //**************************************************************
    public void ReadyToStart()
    {    
            readyButton.SetActive(false);
            ready = true;
            Time.timeScale = 1f;          
    }

    //****************************************************************
    // GameOverShowScore()                                          
    // Show game over panel after each death. Shows the current     
    // score and treat count. Reloads the Gameplay scene with       
    // current score and lives.                                     
    //****************************************************************
    public void GameOverShowScore(int finalScore, int treatFinalScore)
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = finalScore.ToString();
        gameOverTreatText.text = treatFinalScore.ToString();
        StartCoroutine(GameOverReload());

    }

    //****************************************************************
    // GameOverShowScorePlayerDied()                                
    // Show game over panel after final death. Show score and       
    // treats result. Determine if this score is a high score and   
    // show talert text if it is a high score.                      
    //****************************************************************
    public void GameOverShowScorePlayerDied(int finalScore, int treatFinalScore)
    {       
        gameOverPanel.SetActive(true);
        NewHighScoreAlert(finalScore);
        gameOverScoreText.text = finalScore.ToString();
        gameOverTreatText.text= treatFinalScore.ToString();
        StartCoroutine(GameOverLoadMainMenu());       
    }
    
    //****************************************************************
    // GameOverLoadMainMenu()                                       
    // Wait then return to main menu.                               
    //****************************************************************
    IEnumerator GameOverLoadMainMenu()
    {
        yield return new WaitForSeconds(3f);
        MusicController.instance.GetComponent<AudioSource>().UnPause();
        StartCoroutine(SceneFader.instance.LoadLevel("MainMenu"));
    }

    //****************************************************************
    // NewHighScorAlert()                                           
    // Pull the high score from the game manager. Compare the       
    // score parameter to the high score from the game manager. If  
    // current score is higher, change text bar on score panel to   
    // say New High Score. Else, text bar is empty.                 
    //****************************************************************
    private void NewHighScoreAlert(int score)
    {
        List<int> highScoreList = GameManager.instance.LoadHighScoresList();
        int numbersInArray = highScoreList.Count;
        
        if (highScoreList[numbersInArray-1] <= score)
        {          
            newHighScoreAlertText.text = "NEW HIGH SCORE";              
        }
        else
        {
            newHighScoreAlertText.text = "";
        }
    }

    //****************************************************************
    // GameOverReload()                                             
    // Wait then reload the gameplay scene.                         
    //****************************************************************
    IEnumerator GameOverReload()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Gameplay");
    }

    //****************************************************************
    // PauseTheGame()                                               
    // Stop time and show pause panel.                              
    //****************************************************************
    public void PauseTheGame()
    {
        if (!readyButton.activeInHierarchy && !gameOverPanel.activeInHierarchy)
        {

            if (!pausePanel.activeInHierarchy)
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }
        }
    }

    //****************************************************************
    // ResumeTheGame()                                              
    // Restart time and deactivate the pause panel                  
    //****************************************************************
    public void ResumeTheGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    //****************************************************************
    // QuitTheGame()                                                
    // Restart time and return to main menu.                        
    //****************************************************************
    public void QuitGameAndReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    //****************************************************************
    // SetScore()                                                   
    // Set the score text box during gameplay to the score passed   
    // to it                                                        
    //****************************************************************
    public void SetScore (int score)
    {
        scoreText.text = "x" + score;

    }
    
    //****************************************************************
    // SetTreatScore()                                              
    // Set the treat score text box during gameplay to the treat    
    // score passed to it                                           
    //****************************************************************
    public void SetTreatScore (int treatScore)
    {
        treatText.text = "x" + treatScore;
    }

    //****************************************************************
    // SetLifeScore()                                               
    // Set the life score text box during gameplay to the life      
    // score passed to it                                           
    //****************************************************************
    public void SetLifeScore (int lifeScore)
    {
        lifeText.text = "x" + lifeScore;
    }

    //****************************************************************
    // CREATE INSTANCE OF CLASS
    //****************************************************************
    //Instance created so this classes resources can be used by other classes
    public static GameplayController instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //END CREATE INSTANCE

}// END GAMEPLAY CONTROLLER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//****************************************************************
// HIGH SCORE CONTROLLER CLASS
// Sets the high score text box in the highscore scene.
// Manages UI elements of the high score scene.
//****************************************************************
public class HighScoreController : MonoBehaviour
{   
        //DECLARE VARIABLES

        //Create a local highScoreList to fill and manipulate
    private List<int> highScoreList = new List<int>();

        //Serialize variables so that text boxes in HighScore scene can be linked and manipulated
    [SerializeField]
    private Text scoreText;

        //Variable to be put into text box 
    private int highScore;

    //****************************************************************
    // Awake()                                                      
    // Before first frame is rendered make an instance of the       
    // class and set the scenes highscore text box to the           
    // highscore value kept in the GameManager                      
    //****************************************************************
    private void Awake()
    {        
            //Load the highscore list from file into a local list
        highScoreList = GameManager.instance.LoadHighScoresList();
            //Sort in numerical order and then reverse so it is decending order
        highScoreList.Sort();
        highScoreList.Reverse();    

        MakeInstance();
        PrintHighScoreList();
    }

    //****************************************************************
    // PrintHighScoresList()
    // Iterate through list and print out the list in the local
    // scoreText.text box
    //****************************************************************
    public void PrintHighScoreList()
    {
        for (int i = 0; i < highScoreList.Count; i++)
        {
            scoreText.text += (i+1) + ".  \t" + highScoreList[i].ToString() + "\n";

        }
    }

    //****************************************************************
    // GoBackToMainMenu()                                           //
    // Load the Main Menu scene. Utilized via a button in Unity     //
    // Editor                                                       //
    //****************************************************************
    public void GoBackToMainMenu()
    {
        StartCoroutine(SceneFader.instance.LoadLevel("MainMenu"));
    }

    //****************************************************************
    // CREATE INSTANCE OF CLASS
    //
    //****************************************************************
    //Create instance so that passing this classes resources are easily available to other classes
    public static HighScoreController instance;

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //END MAKE INSTANCE

}// HighScoreController

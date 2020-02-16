using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//****************************************************************
// OPTIONS CONTROLLER CLASS
// Orchastrates the user interface for the options scene.
//****************************************************************

public class OptionsController : MonoBehaviour

{
    //DECLARE VARIABLES
    //Flags to represent difficulty level of game
    [SerializeField]
    private GameObject easySign, mediumSign, hardSign;

    //****************************************************************
    // Start()
    // Call SetTheDifficulty() funtion
    //****************************************************************
    void Start()
    {
   //     SetTheDifficulty();
    }

    //****************************************************************
    // SetTheTreatFlag()
    // Depending on what is passed to the function, the treat flag
    // will be active on the appropriate button
    //***************************************************************
    void SetTheTreatFlag(string difficulty)
    {
        switch (difficulty)
        {
            case "easy":
                easySign.SetActive(true);
                mediumSign.SetActive(false);
                hardSign.SetActive(false);
                break;

            case "medium":
                easySign.SetActive(false);
                mediumSign.SetActive(true);
                hardSign.SetActive(false);
                break;

            case "hard":
                easySign.SetActive(false);
                mediumSign.SetActive(false);
                hardSign.SetActive(true);
                break;
        }
    }

    //****************************************************************
    // SetTheDifficulty()
    // Set the difficulty based on what is set in the game
    // preferences 
    //****************************************************************
    void SetTheDifficulty()
    {
        if (GamePreferences.getEasyDifficulty() == 1)
        {
            SetTheTreatFlag("easy");
        }

        if (GamePreferences.getMediumDifficulty() == 1)
        {
            SetTheTreatFlag("medium");
        }

        if (GamePreferences.getHardDifficulty() == 1)
        {
            SetTheTreatFlag("hard");
        }
    }

    //****************************************************************
    // EasyDifficulty()
    // Set the treat flag and save the prefered difficulty in game
    // preferences
    //****************************************************************
    public void EasyDifficulty()
    {
        SetTheTreatFlag("easy");
        GamePreferences.setEasyDifficulty(1);     
        GamePreferences.setMediumDifficulty(0);
        GamePreferences.setHardDifficulty(0);
    }

    //****************************************************************
    // MediumDifficulty()
    // Set the treat flag and save the prefered difficulty in game
    // preferences
    //****************************************************************
    public void MediumDifficulty()
    {
        SetTheTreatFlag("medium");
        GamePreferences.setEasyDifficulty(0);
        GamePreferences.setMediumDifficulty(1);     
        GamePreferences.setHardDifficulty(0);
    }

    //****************************************************************
    // HardDifficulty()
    // Set the treat flag and save the prefered difficulty in game
    // preferences
    //****************************************************************
    public void HardDifficulty()
    {
        SetTheTreatFlag("hard");
        GamePreferences.setEasyDifficulty(0);
        GamePreferences.setMediumDifficulty(0);
        GamePreferences.setHardDifficulty(1);      
    }

    //****************************************************************
    // GoBackToMainMenu()
    // Load main menu scene
    //****************************************************************
    public void GoBackToMainMenu()
    {
        StartCoroutine(SceneFader.instance.LoadLevel("MainMenu"));
    }
}

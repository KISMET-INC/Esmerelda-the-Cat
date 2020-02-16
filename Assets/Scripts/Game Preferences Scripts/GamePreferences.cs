using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//****************************************************************
// GAME PREFERENCES CLASS
// Declare, get and set PlayerPrefs
// Includes difficulty settings, highscores, and player pref 
// for music on or off
//****************************************************************
public static class GamePreferences
{
    //DECLARE VARIABLES
    public static string EasyDifficulty = "EasyDifficulty";
    public static string MediumDifficulty = "MediumDifficulty";
    public static string HardDifficulty = "HardDifficulty";

    public static string EasyDifficultyHighScore = "EasyDifficultyHighScore";
    public static string MediumDifficultyHighScore = "MediumDifficultyHighScore";
    public static string HardDifficultyHighScore = "HardDifficultyHighScore";

    public static string EasyDifficultyTreatScore = "EasyDifficultyTreatScore";
    public static string MediumDifficultyTreatScore = "MediumDifficultyTreatScore";
    public static string HardDifficultyTreatScore = "HardDifficultyTreatScore";

    public static string MusicState = "MusicState";



    //****************************************************************
    // EASY DIFFICULTY ()
    // Get and Set
    //****************************************************************
    public static int getEasyDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficulty);
    }

    public static void setEasyDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficulty, state);
    }


    //****************************************************************
    // MEDIUM DIFFICULTY ()
    // Get and Set
    //****************************************************************
    public static int getMediumDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficulty);
    }

    public static void setMediumDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficulty, state);
    }


    //****************************************************************
    // HARD DIFFICULTY ()
    // Get and Set
    //****************************************************************
    public static int getHardDifficulty()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficulty);
    }

    public static void setHardDifficulty(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficulty, state);
    }


    //****************************************************************
    // EASY DIFFICULTY HIGH SCORE ()
    // Get and Set
    //**************************************************************** 
    public static int getEasyDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyHighScore);
    }

    public static void setEasyDifficultyHighScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficultyHighScore, state);
    }


    //****************************************************************
    //  MEDIUM DIFFICULTY HIGH SCORE ()
    // Get and Set
    //****************************************************************
    public static int getMediumDifficultyHighScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyHighScore);
    }

    public static void setMediumDifficultyHighScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficultyHighScore, state);
    }


    //****************************************************************
    // HARD DIFFICULTY HIGH SCORE ()
    // Get and Set
    //****************************************************************
    public static int getHardDifficultyScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficultyHighScore);
    }

    public static void setHardDifficultyScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficultyHighScore, state);
    }


    //****************************************************************
    // EASY DIFFICULTY TREAT SCORE ()
    // Get and Set
    //****************************************************************
    public static int getEasyDifficultyTreatScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyTreatScore);
    }

    public static void setEasyDifficultyTreatScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.EasyDifficultyTreatScore, state);
    }


    //****************************************************************
    // MEDIUM DIFFICULTY TREAT SCORE ()
    // Get and Set
    //****************************************************************
    public static int getMediumDifficultyTreatScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyTreatScore);
    }

    public static void setMediumDifficultyTreatScore(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.MediumDifficultyTreatScore, state);
    }


    //****************************************************************
    // HARD DIFFICULTY TREAT SCORE ()
    // Get and Set
    //****************************************************************
    public static int getHardDifficultyTreatScore()
    {
        return PlayerPrefs.GetInt(GamePreferences.HardDifficultyTreatScore);
    }

    public static void setHardDifficultyTreatScore(int state) 
    {
        PlayerPrefs.SetInt(GamePreferences.HardDifficultyTreatScore, state);
    }


    //****************************************************************
    // IS MUSIC ON ()
    // Get and Set
    //****************************************************************
    public static int getMusicState()
    {
        return PlayerPrefs.GetInt(GamePreferences.MusicState);
    }

    public static void setMusicState(int state)
    {
        PlayerPrefs.SetInt(GamePreferences.MusicState,state);
    }


} // END GAME PREFERENCES

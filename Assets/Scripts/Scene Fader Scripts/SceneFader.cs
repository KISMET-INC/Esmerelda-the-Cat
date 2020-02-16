using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//****************************************************************
// SCENE FADER CLASS
// Controls the fade in and fade out between the scenes
//****************************************************************
public class SceneFader : MonoBehaviour
{
   
   
        //DECLARE VARIABLES
    [SerializeField]
    public GameObject fadePanel;

    [SerializeField]
    public  Animator fadeAnim;


    private void Awake()
    {
        MakeSingleton();
        
    }
    //****************************************************************
    // LoadLevel()
    // Activate fade panel. Play fade out animation. Load new 
    // scene. Play fade in animation. Pass to deactivatePanel() 
    // coroutine
    //****************************************************************
    public IEnumerator LoadLevel (string level)
    {
        fadePanel.SetActive(true);
        fadeAnim.Play("FadeOut");
        yield return new WaitForSecondsRealtime(.25f);
        SceneManager.LoadScene(level);
        fadeAnim.SetBool("FadeIn", true);
        StartCoroutine(deactivatePanel());
    }
    //****************************************************************
    // deactivatePanel()
    // WaitforSeconds then deactivate fade panel
    //****************************************************************
    public IEnumerator deactivatePanel()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        fadePanel.SetActive(false);
      
    }


    //****************************************************************
    // CREATE SINGLETON INSTANCE
    //****************************************************************
    public static SceneFader instance;
    public void MakeSingleton()
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


} // END SCENE FADER

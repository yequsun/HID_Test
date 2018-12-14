using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelection : MonoBehaviour {

   void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            loadHome();
        }
    }

   public void loadLevel1()
    {
        SceneManager.LoadScene("Song-1");
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene("Song-2");
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene("Song-3");
    }
    public void loadHome()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

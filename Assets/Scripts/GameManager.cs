using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HeartSystem heartSystem;

    public GameObject victory;

    private void FixedUpdate()
    {
        //if(heartSystem.health<1)
        //{
        //    EndGame();
       // }
    }
    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }
    
    public void Quit()
    {
        Debug.Log("Application Quit");
        Application.Quit();

    }
    public void Retry2()
    {
        heartSystem.health = 3;
        SceneManager.LoadScene("SunnyLand2");
        
        Time.timeScale = 1f;
    }
    public void Retry()
    {
        heartSystem.health = 3;
        //SceneManager.LoadScene("Sunny Land 2");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
       
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void NextScene()
    {
        Debug.Log("PlayeScence 1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Victory()
    {
        victory.SetActive(true);
    }
}

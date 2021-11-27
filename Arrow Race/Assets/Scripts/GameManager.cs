using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool LevelStarted;
    public bool LevelEndGame;
    public bool levelFinished;
    public bool playerDied;
    public bool isNextLevel, isSameLevel;
    public bool levelendspiderdied = false;
    public int currentcloneno, lastcloneno;
    public float arrow_cooldown = 0.5f;


    private void Awake() {
        MakeInstance();
        CheckPlayerPrefs();
    }


public void CheckPlayerPrefs(){
        if (PlayerPrefs.HasKey("Gold")){
           GameplayManager.instance.goldNo = PlayerPrefs.GetInt("Gold");
        }
        else{
            
            PlayerPrefs.SetInt("Gold", 0);
            GameplayManager.instance.goldNo = 0;
            PlayerPrefs.Save();

        }
        if (PlayerPrefs.HasKey("Level")){
           GameplayManager.instance.levelNo = PlayerPrefs.GetInt("Level");

        }
        else{
             PlayerPrefs.SetInt("Level", 1);
             GameplayManager.instance.levelNo = 1;
            PlayerPrefs.Save();
        }
     }


    public void NextLevel(){
        levelFinished = false;
        playerDied = false;
        LevelStarted = false;
        Time.timeScale = 1f;
        StartCoroutine(LevelManager.instance.SceneAsyn(sceneselection()));
    }
    public void Restart()
    {
        playerDied = false;
        LevelStarted = false;
        Time.timeScale = 1f;
        StartCoroutine(LevelManager.instance.SceneAsyn(SceneManager.GetActiveScene().name));
       // sceneselection();
        
    }
    string sceneselection(){
        if(SceneManager.GetActiveScene().name == "Level1"){
            return "Level2";
        }
        else if (SceneManager.GetActiveScene().name == "Level2"){
            return "Level1";
        }
        else
        {
            return "Level2";
        }
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (instance != this) {
        Destroy (gameObject);
    }    
   }
   
    



}

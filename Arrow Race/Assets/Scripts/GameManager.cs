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
    private string[] SceneNames;
    


    private void Awake() {
        
        MakeInstance();
        CheckPlayerPrefs();
    }

    private void Start()
    {
        SceneNames = new string[4];
        SceneNames[0] = "Level1";
        SceneNames[1] = "Level2";
        SceneNames[2] = "Level3";
        SceneNames[3] = "Level4";
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

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                return "Level2";
            case "Level2":
                return "Level3";
            case "Level3":
                return "Level4";
            case "Level4":
                return "Level1";
            default:
                return "Level1";
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

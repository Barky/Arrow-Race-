using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool LevelStarted;
    public bool LevelEndGame;


    private void Awake()
    {
        MakeSingleton();
        

    }
    private void Start() {
        CheckPlayerPrefs();
    }
    private void Update() {
        Debug.Log(LevelStarted+ " dönüyor hala");
        if (!LevelStarted){
            gameStart();
        }
    }
    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void gameStart(){
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0)){
            LevelStarted = true;
        }
    }

    public void CheckPlayerPrefs(){
        if (!PlayerPrefs.HasKey("Gold")){
            PlayerPrefs.SetInt("Gold", 0);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("Level")){
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.Save();

        }
        
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

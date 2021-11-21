using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool LevelStarted;
    public bool LevelEndGame;
    public bool levelFinished;
    public bool playerDied;
    public bool isNextLevel, isSameLevel;
    private Text goldText, panelgoldlevelui,panelgoldtotalui, levelText;
    private GameObject levelstartui, winpanel, losepanel, in_level_panel;
    public int currentcloneno, lastcloneno;
    private int goldNo, levelNo;
    private string canvasshortcut = "/UICamera/Canvas";
    private bool endfunc_temp = true;
    private void Awake()
    {
        MakeSingleton();
        CheckPlayerPrefs();


    }
    private void Start() {
        levelstartui = GameObject.Find(canvasshortcut + "/levelStart");
        goldText = GameObject.Find(canvasshortcut + "/in_level_panel/goldbar/Text").GetComponent<Text>();
        in_level_panel = GameObject.Find(canvasshortcut + "/in_level_panel");
        winpanel = GameObject.Find(canvasshortcut + "/Win");
        losepanel = GameObject.Find(canvasshortcut + "/Lose");
        levelText = GameObject.Find(canvasshortcut + "/in_level_panel/Level/LevelNo").GetComponent<Text>();
        panelgoldtotalui = GameObject.Find(canvasshortcut + "/Win/Coin/Text").GetComponent<Text>(); // top right corner
        panelgoldlevelui = GameObject.Find(canvasshortcut + "/Win/Background/Text").GetComponent<Text>(); // middle
        
    }
    private void Update() {
        Debug.Log("goldno var: "+goldNo);
        Debug.Log("playerprefs var: "+PlayerPrefs.GetInt("Gold"));
        Debug.Log("level var: "+levelNo);
         goldText.text = goldNo.ToString();

         if (playerDied){
        Debug.Log("youlose çalıştı");
        Time.timeScale = 0;
        in_level_panel.gameObject.SetActive(false);
        losepanel.gameObject.SetActive(true);
             //StartCoroutine(youlose());
         }
        if (!LevelStarted && !levelFinished){
            levelText.text = levelNo.ToString();
            gameStart();
        }
        if (levelFinished){
            LevelEnded();
            
        }
        
        
       
    }
    public void LevelEnded(){
        
        if(endfunc_temp){
        levelNo ++;
        Time.timeScale = 0;
        winpanel.gameObject.SetActive(true);
        UpdatePlayerPrefs();
        Debug.Log("LevelStarted: "+LevelStarted+"LevelEndGame :"+ LevelEndGame+ "levelFinished: "+levelFinished+"playerdied :"+ playerDied);
        panelgoldlevelui.text = "+ " + goldNo.ToString();
        panelgoldtotalui.text = PlayerPrefs.GetInt("Gold").ToString();
        endfunc_temp = false;
        }
    }
    public void IncrementGold()
    {
        goldNo++;
        goldText.text = goldNo.ToString();
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
            levelstartui.SetActive(false);

        }
    }
    public void UpdatePlayerPrefs(){ // level end'de çağır lütf
        if(PlayerPrefs.GetInt("Gold") != goldNo){
            int newGold = PlayerPrefs.GetInt("Gold") + goldNo;
            PlayerPrefs.SetInt("Gold", newGold);
            PlayerPrefs.Save();
        }
        if(PlayerPrefs.GetInt("Level") != levelNo){
            int newlevel = levelNo;
            PlayerPrefs.SetInt("Level", levelNo);
            PlayerPrefs.Save();
        }
    }
    public void CheckPlayerPrefs(){
        if (PlayerPrefs.HasKey("Gold")){
            goldNo = PlayerPrefs.GetInt("Gold");
        }
        else{
            
            PlayerPrefs.SetInt("Gold", 0);
            goldNo = 0;
            PlayerPrefs.Save();

        }
        if (PlayerPrefs.HasKey("Level")){
           levelNo = PlayerPrefs.GetInt("Level");

        }
        else{
             PlayerPrefs.SetInt("Level", 1);
             levelNo = 1;
            PlayerPrefs.Save();
        }
        Debug.Log("gold= "+goldNo+" level: "+levelNo);
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }
    void OnSceneWasLoaded (Scene scene, LoadSceneMode mode)
    {
        
        
            levelNo = PlayerPrefs.GetInt("Level");
            goldNo = PlayerPrefs.GetInt("Gold");

        

    }
    public void NextLevel(){
        levelFinished = false;
         SceneManager.LoadSceneAsync("Level1");
    }
    public void Restart()
    {
        levelFinished = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}

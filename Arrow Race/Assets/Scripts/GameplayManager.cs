using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    private bool endfunc_temp = true;
    private Text goldText, panelgoldlevelui,panelgoldtotalui, levelText;
    private GameObject levelstartui, winpanel, losepanel, in_level_panel, pausebutton;
    private GameObject loadingpanel;

    
    public int goldNo = 0, levelNo, inlevelgoldno;
    private string canvasshortcut = "/UICamera/Canvas";

    public Transform playerprefab;

    public GameObject Loadingpanel { get => loadingpanel; set => loadingpanel = value; }

    private void Awake() {
        MakeInstance();
        levelstartui = GameObject.Find(canvasshortcut + "/levelStart");
        goldText = GameObject.Find(canvasshortcut + "/in_level_panel/goldbar/Text").GetComponent<Text>();
        in_level_panel = GameObject.Find(canvasshortcut + "/in_level_panel");
        winpanel = GameObject.Find(canvasshortcut + "/Win");
        losepanel = GameObject.Find(canvasshortcut + "/Lose");
        pausebutton = GameObject.Find(canvasshortcut + "/in_level_panel/Pause");
        levelText = GameObject.Find(canvasshortcut + "/in_level_panel/Level/LevelNo").GetComponent<Text>();
        panelgoldtotalui = GameObject.Find(canvasshortcut + "/Win/Coin/Text").GetComponent<Text>(); // top right corner
        panelgoldlevelui = GameObject.Find(canvasshortcut + "/Win/Background/Text").GetComponent<Text>(); // middle
        pausebutton.SetActive(false);
        // loadingpanel = GameObject.Find(canvasshortcut + "/");
    }
    void gameStart(){
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0)){
            GameManager.instance.LevelStarted = true;
            levelstartui.SetActive(false);
            pausebutton.SetActive(true);

        }}
    private void Update() {
        if (GameManager.instance.playerDied){

            Time.timeScale = 0;

            in_level_panel.gameObject.SetActive(false);
            losepanel.gameObject.SetActive(true);
        }
        if (!GameManager.instance.LevelStarted && !GameManager.instance.levelFinished){
            levelText.text = levelNo.ToString();
            gameStart();
        }
        if (GameManager.instance.levelFinished){
            LevelEnded();
            
        }
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += onSceneWasLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= onSceneWasLoaded;
    }
    void onSceneWasLoaded(Scene scene, LoadSceneMode mode){
        CreatePlayer();
    inlevelgoldno = 0;
    levelNo = PlayerPrefs.GetInt("Level");
    goldNo = PlayerPrefs.GetInt("Gold");
    goldText.text = goldNo.ToString();
    levelText.text = levelNo.ToString();
    GameManager.instance.LevelStarted = false;
    GameManager.instance.levelFinished = false;

    Debug.Log("sceneloaded ??al????t??. levelno: "+levelNo);
    }

    public void IncrementGold()
    {
        goldNo++;
        inlevelgoldno++;
        goldText.text = goldNo.ToString();
    }
    public void LevelEnded(){
        
        if(endfunc_temp){
        
        endfunc_temp = false;
            StartCoroutine(waittwosec());
        }
    }
    IEnumerator waittwosec()
    {
        pausebutton.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        levelNo++;
        Time.timeScale = 0;
        winpanel.gameObject.SetActive(true);
        UpdatePlayerPrefs();

        panelgoldlevelui.text = "+ " + inlevelgoldno.ToString();
        panelgoldtotalui.text = PlayerPrefs.GetInt("Gold").ToString();
    }
    void UpdatePlayerPrefs(){ // level end'de ??a????r l??tf
        if(goldNo != PlayerPrefs.GetInt("Gold")){
            int newGold = PlayerPrefs.GetInt("Gold") + inlevelgoldno;
            PlayerPrefs.SetInt("Gold", newGold);
            PlayerPrefs.Save();
        }
        if(levelNo != PlayerPrefs.GetInt("Level")){
            int newlevel = levelNo;
            PlayerPrefs.SetInt("Level", levelNo);
            PlayerPrefs.Save();
        }
    }
    void CreatePlayer()
    {
        Transform player = Instantiate(playerprefab, Vector3.zero, Quaternion.identity);
        player.name = "Player";
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

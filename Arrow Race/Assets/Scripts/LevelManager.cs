using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private GameObject loadingscreen;


    void Awake()
    {
        MakeInstance();

        loadingscreen = GameObject.Find("/UICamera/Canvas/Loadingscreen");
    }

    public IEnumerator SceneAsyn(string scenename)
    {
        loadingscreen.SetActive(true);
        AsyncOperation operat = SceneManager.LoadSceneAsync(scenename);

        while (!operat.isDone)
        {
            Debug.Log("çalýþýo loading panel");
            yield return null;
        }
    }
    
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}

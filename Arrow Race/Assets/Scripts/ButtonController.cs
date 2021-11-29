using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private bool gamepaused = false;
    public GameObject pausepanel;
    public void LoseRestartLevel()
    {
        GameManager.instance.Restart();
        
    }

    public void NextLevel(){
        GameManager.instance.NextLevel();
    }

    public void Pausebt()
    {
        switch (gamepaused)
        {
            case true:
                pausepanel.SetActive(false);
                Time.timeScale = 1f;
                gamepaused = false;
                break;

            case false:
                pausepanel.SetActive(true);
                Time.timeScale = 0f;
                gamepaused = true;
                break;
        }
    }
}

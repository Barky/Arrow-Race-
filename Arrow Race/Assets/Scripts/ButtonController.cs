using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void LoseRestartLevel()
    {
        GameManager.instance.Restart();
        
    }

    public void NextLevel(){
        GameManager.instance.NextLevel();
    }
}

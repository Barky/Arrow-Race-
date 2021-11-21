using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{

    
    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            GameManager.instance.LevelEndGame = true;
            
        }
    }
}

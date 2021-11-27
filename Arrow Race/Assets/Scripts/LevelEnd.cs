using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{

    Slider levelslider;
    
    
    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            GameManager.instance.LevelEndGame = true;
            if (levelslider.value != 2f)
            {
                levelslider.value = 2f;
            }
        }
    }
    
    void Start()
    {
        levelslider = GameObject.Find("/UICamera/Canvas/in_level_panel/level_bar").GetComponent<Slider>();

    }
}

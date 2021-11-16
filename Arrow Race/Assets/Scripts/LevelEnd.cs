using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{

    public Image end;
    private void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
        {
            Debug.Log("level biti cnm");
            end.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}

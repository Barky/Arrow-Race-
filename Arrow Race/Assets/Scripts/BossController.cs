using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    private TextMeshPro healthUI;
    private int bossHealth;
    private int minBossHealth = 5, maxBossHealth = 10;

    private void Awake() {
        healthUI = GameObject.Find("/Spider_Queen/Text").GetComponent<TextMeshPro>();
        bossHealth = Random.Range(minBossHealth, maxBossHealth);
        
    }
    private void Update() {
        healthUI.text = bossHealth.ToString();
        if (bossHealth == 0)
        {
            Destroy(gameObject);
            GameManager.instance.LevelStarted = false;
            GameManager.instance.LevelEndGame = false;
            GameManager.instance.levelFinished = true;
        }
    }

    private void OnTriggerEnter(Collider target) {
        if(target.tag == "Weapon" && GameManager.instance.LevelEndGame){
            bossHealth--;
        }
    
    }
        
}

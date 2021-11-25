using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    private TextMeshPro healthUI;
    private int bossHealth;
    private float newx;
    private int minBossHealth = 10, maxBossHealth = 20;

    private void Awake() {
        healthUI = GameObject.Find(this.gameObject.name + "/Text").GetComponent<TextMeshPro>();
        bossHealth = Random.Range(minBossHealth, maxBossHealth);
        //transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        newx = transform.position.x;
        healthUI.text = bossHealth.ToString();

    }
    private void Update() {

        Debug.Log(bossHealth);
        // if(newx == -5f){
        //     newx = transform.position.x +1f*Time.deltaTime;
        // }
        // else if(newx == 5f){
        //     newx = transform.position.x -1f*Time.deltaTime;
        // }
        // newx = Mathf.Clamp(newx, -5f, 5f);
        // transform.position = new Vector3(newx, transform.position.y, transform.position.z);
        
        if (bossHealth == 0)
        {
            Destroy(healthUI);
            StartCoroutine(destroyin());
            
        
}    }
    IEnumerator destroyin(){
        GameManager.instance.LevelStarted = false;
        GameManager.instance.LevelEndGame = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
        
        GameManager.instance.levelFinished = true;
    }
    private void OnTriggerEnter(Collider target) {
        if(target.tag == "Weapon" && GameManager.instance.LevelEndGame){
            bossHealth--;
            healthUI.text = bossHealth.ToString();
        }
    
    }
    
}

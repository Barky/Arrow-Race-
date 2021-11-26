using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController : MonoBehaviour
{
    public Transform shotFX, dieFX;
    private Transform FXParent;
    private TextMeshPro healthUI;
    private int bossHealth;
    private float newx;
    private int minBossHealth = 40, maxBossHealth = 100;
    Animator playeranim, cloneanim;

    private void Awake() {
        healthUI = GameObject.Find(this.gameObject.name + "/Text").GetComponent<TextMeshPro>();
        bossHealth = Random.Range(minBossHealth, maxBossHealth);
        //transform.position = new Vector3(-5f, transform.position.y, transform.position.z);
        newx = transform.position.x;
        healthUI.text = bossHealth.ToString();
        playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void Start()
    {
        FXParent = GameObject.Find("/Particles").transform;
    }
    private void Update() {

        Debug.Log(bossHealth);
        // yürütemedim hala amk
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
            StartCoroutine(diefx());
            StartCoroutine(destroyin());
            
        
}    }
    IEnumerator shotfx()
    {
        Transform fxcreated;
        Vector3 fxpos = transform.position;
        fxpos.z -= 1f;
        fxcreated = Instantiate(shotFX, fxpos, Quaternion.identity);
        fxcreated.name = "shotFX";
        fxcreated.parent = FXParent;
        yield return new WaitForSeconds(0.5f);
        Destroy(fxcreated.gameObject);
    }
    IEnumerator diefx()
    {
        Transform fxcreated;
        Vector3 fxpos = transform.position;
        fxpos.z -= 1f;
        fxcreated = Instantiate(dieFX, fxpos, Quaternion.identity);
        fxcreated.name = "dieFX";
        fxcreated.parent = FXParent;
        yield return new WaitForSeconds(1f);
        Destroy(fxcreated.gameObject);
    }
    IEnumerator destroyin(){
        playeranim.SetBool("levelEnd", false);
        GameManager.instance.LevelStarted = false;
        GameManager.instance.LevelEndGame = false;
        GameManager.instance.levelFinished = true;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        
        
        

    }
    private void OnTriggerEnter(Collider target) {
        if(target.tag == "Weapon" && GameManager.instance.LevelEndGame && bossHealth > 0){
            StartCoroutine(shotfx());
            bossHealth--;
            healthUI.text = bossHealth.ToString();
        }
    
    }
    
}

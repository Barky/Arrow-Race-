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
    private float movementspeed = 1.6f;
    private string side = "left";
    private int minBossHealth = 40, maxBossHealth = 100;
    Animator playeranim, cloneanim;
    Vector3 swerving;
    private void Awake() {
        healthUI = GameObject.Find(this.gameObject.name + "/Text").GetComponent<TextMeshPro>();
        bossHealth = Random.Range(minBossHealth, maxBossHealth);
        healthUI.text = bossHealth.ToString();
        playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
    private void Start()
    {
        FXParent = GameObject.Find("/Particles").transform;
    }
    private void Update() {
        //if (bossHealth > 0 && GameManager.instance.LevelEndGame)
        //{
        //    bossMovement();
        //}
        bossMovement();
        if (bossHealth == 0)
        {
            Destroy(healthUI);
            StartCoroutine(diefx());
            StartCoroutine(destroyin());


        }
    
    
    }

    void bossMovement()
    {
        
        switch (side)
        {
            case "left":
                swerving = new Vector3(transform.position.x - movementspeed * Time.deltaTime, transform.position.y, transform.position.z);
                break;
            case "right":
                swerving = new Vector3(transform.position.x + movementspeed * Time.deltaTime, transform.position.y, transform.position.z);
                break;
        }
        transform.position = swerving;
            if (side == "left" && transform.position.x < -4.7)
            {
                side = "right";
            }
            else if (side == "right" && transform.position.x > 4.7)
            {
                side = "left";
            }

        
        

    }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    Animator m_anim;

    public Transform shotFX, dieFX, spawnFX;


    public static EnemyController instance;
    ClonePositionControler clonePositionControler;
    private TextMeshPro healthText;
    private int minHealth = 2, maxHealth = 6, health;
    private Transform player;
    private int cloneNo;
    private float swervespeed = 2f, movementSpeed = 10f;
    private bool playerspawned = false;
    private Transform FXParent;
    [SerializeField]
    private Transform arrowPrefab;

    private Transform arrowparent;

    Vector3 arrowpos;

    Vector3 spawnposition;

    private float arrowx=0f, minarrowy = 0.5f, maxarrowy = 1.2f, arrowy, arrowz = 2.5f;

    private float arrow_cooldown;




    private void Awake()
    {
        instance = this;
        m_anim = GetComponent<Animator>();
        health = Random.Range(minHealth, maxHealth);
        FXParent = GameObject.Find("/Particles").transform;
        
        string nameOfParent = gameObject.name;
        string nameOfchild = "Text";
        string nameofChunk, childLocation;
        if (gameObject.transform.parent)
        {
            nameofChunk = gameObject.transform.parent.name;
            childLocation = "/" + nameofChunk + "/" + nameOfParent + "/" + nameOfchild;
        }
        else
        {
             childLocation =  "/" + nameOfParent + "/" + nameOfchild;
        }
        healthText = GameObject.Find(childLocation).GetComponent<TextMeshPro>();


    }

    private void Start()
    {
        health = Random.Range(minHealth, maxHealth);
        arrow_cooldown = GameManager.instance.arrow_cooldown;
    }
    private void Update()
    {

        if (!player)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;

            clonePositionControler = GameObject.FindGameObjectWithTag("Player").GetComponent<ClonePositionControler>();
            return;
        }


        if (healthText){
        healthText.text = health.ToString();
        }

        
        if (gameObject.tag == "PlayerClone"){

            m_anim.SetBool("isCloned", PlayerController.instance.anim_gamestarted);
            m_anim.SetBool("LevelEnd", PlayerController.instance.anim_levelend);
            arrowy = Random.Range(minarrowy, maxarrowy);
            if (cloneNo > 0)
            {
                FillintheBlanks(cloneNo);
            }
            // transform.position = clonePositionControler.cloneBehaviour[cloneNo].Cube.transform.position;

        }
    }

    void FillintheBlanks(int no){
        for (int i = 0 ; i < cloneNo; i++)
        {
            if (!clonePositionControler.cloneBehaviour[i].IsFull){

                clonePositionControler.cloneBehaviour[i].IsFull = true;
                clonePositionControler.cloneBehaviour[no].IsFull = false;
                cloneNo = i;
                transform.position = clonePositionControler.cloneBehaviour[i].Cube.transform.position;
            }
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Weapon")
        {

            health--;
            StartCoroutine(shotfx());
            if (health == 0)
            {
                if (gameObject.tag == "EnemyDummy")
                {
                    StartCoroutine(diefx());
                    Destroy(gameObject);
                }

                else if (gameObject.tag == "EnemyPlayer")
                {

                    StartCoroutine(diefx());
                    
                    StartCoroutine(spawning());
                    StartCoroutine(constantShoot());

                }
            }
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
        yield return new WaitForSeconds(1f);
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
        yield return new WaitForSeconds(2f);
        Destroy(fxcreated.gameObject);
    }
    IEnumerator spawning()
    {
        Destroy(healthText);
        transform.SetParent(player, true);
        transform.tag = "PlayerClone";
        transform.localScale = new Vector3(0, 0, 0);
        
        for (int i = 0; i < clonePositionControler.cloneBehaviour.Count; i++)
        {
            if (!clonePositionControler.cloneBehaviour[i].IsFull)
            {

                cloneNo = i;
                break;
            }
        }
        clonePositionControler.cloneBehaviour[cloneNo].IsFull = true;
        Transform fxcreated;
        Vector3 fxpos = clonePositionControler.cloneBehaviour[cloneNo].Cube.transform.position;
        fxcreated = Instantiate(spawnFX, fxpos, Quaternion.identity);
        fxcreated.name = "spawnFX";
        fxcreated.parent = FXParent;
        yield return new WaitForSeconds(0.4f);
        transform.localScale = new Vector3(1, 1, 1);
        transform.position = clonePositionControler.cloneBehaviour[cloneNo].Cube.transform.position;
        yield return new WaitForSeconds(2f);
        Destroy(fxcreated.gameObject);
    }
    IEnumerator constantShoot()
    {
       
            while (true)
            {
                arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
                Transform newArrow = (Transform)Instantiate(arrowPrefab, arrowpos, Quaternion.identity);
                newArrow.parent = gameObject.transform;
                yield return new WaitForSeconds(arrow_cooldown);
            }
    }
            
    private void OnCollisionEnter(Collision target)
    {
        if (gameObject.tag == "PlayerClone" && target.gameObject.tag == "EnemyPlayer"  )
        {
            clonePositionControler.cloneBehaviour[cloneNo].IsFull = false;
            Destroy(gameObject);
            
        }
        if (gameObject.tag == "PlayerClone" && target.gameObject.tag == "Obstacle"  )
        {
            clonePositionControler.cloneBehaviour[cloneNo].IsFull = false;
            Destroy(gameObject);

        }
        if (gameObject.tag == "EnemyPlayer" && target.gameObject.tag == "Player")
        {
            GameManager.instance.playerDied = true;
            // level bitme scripti yaz
        }

        
    }

}

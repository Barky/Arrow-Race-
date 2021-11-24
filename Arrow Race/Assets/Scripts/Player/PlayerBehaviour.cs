using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    private bool isGameStarted, spiderspawned = true;
    [SerializeField]
    private Transform arrowPrefab;

    private Transform arrowparent;

    private Vector3 bossposition, arrowpos;
    
    public Transform boss;

    private Transform Levelend;


    private float arrowx=0f, arrowy = 1f, arrowz = 2.5f, firstpos;

    private float  arrow_cooldown = 0.3f;

    private void Awake()
    {
       // _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
        Levelend = GameObject.Find("/LevelEnd").transform;
        arrowparent = transform;
        firstpos = transform.position.z;

    }
    private void Start()
    {
        bossposition = new Vector3(5f, 0.89f, 177f);
        StartCoroutine(constantShoot());
        
        
        
    }
    private void Update() {
        if (Levelend.transform.position.z -transform.position.z > 50f && spiderspawned)
        {

            Transform bossins = Instantiate(boss, bossposition, Quaternion.identity);
            bossins.name = "spider";
            bossins.Rotate(0f, 180f, 0f, Space.Self);
            spiderspawned = false;
        }
    }
    
        private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "EnemyPlayer" || target.gameObject.tag == "EnemyDummy")
        {
            if(GameManager.instance.currentcloneno == 0 && gameObject.tag == "Player"){
               GameManager.instance.playerDied = true;
            }
            
        }
    }
    
    IEnumerator constantShoot()
    {
       
            while (true)
            {
                if(GameManager.instance.LevelStarted)
                {
                arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
                Transform newArrow = (Transform)Instantiate(arrowPrefab, arrowpos, Quaternion.identity);
                newArrow.parent = gameObject.transform;
                yield return new WaitForSeconds(arrow_cooldown);
                }
                else
                {
                    yield return null;            
                }
            }
    }
    }
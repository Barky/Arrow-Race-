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


    private float arrowx=0f, minarrowy = 0.5f, maxarrowy = 1.2f, arrowy, arrowz = 2.5f, firstpos;

    private float arrow_cooldown;

    private void Awake()
    {
        
       // _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        Levelend = GameObject.Find("/LevelEnd").transform;
        arrowparent = transform;
        firstpos = transform.position.z;

    }
    private void Start()
    {
        bossposition = new Vector3(5f, 0.89f, 177f);
        StartCoroutine(constantShoot());
        arrow_cooldown = GameManager.instance.arrow_cooldown;


    }
    private void Update() {
        arrowy = Random.Range(minarrowy, maxarrowy);
        arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
        if (Levelend.transform.position.z -transform.position.z < 120f && spiderspawned)
        {

            Transform bossins = Instantiate(boss, bossposition, Quaternion.identity);
            bossins.name = "spider";
            bossins.Rotate(0f, 180f, 0f, Space.Self);
            spiderspawned = false;
        }
    }
    
        private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "EnemyPlayer" || target.gameObject.tag == "EnemyDummy" || target.gameObject.tag == "Obstacle")
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
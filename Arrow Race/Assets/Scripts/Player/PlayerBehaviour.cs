using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    private bool isGameStarted;
    [SerializeField]
    private Transform arrowPrefab;

    private Transform arrowparent;

    Vector3 arrowpos;



    private float arrowx=0f, arrowy = 2.5f, arrowz = 2.5f;

    private float  arrow_cooldown = 0.7f;

    private void Awake()
    {
       // _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
        arrowparent = transform;

    }
    private void Start()
    {
        
        StartCoroutine(constantShoot());
        
        
        
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{

    ArrowController arrowcont;
    ClonePositionControler clonePositionControler;
    private TextMeshPro healthText;
    private int minHealth = 2, maxHealth = 6, health;
    private Transform player;
    private int cloneNo;

    [SerializeField]
    private Transform arrowPrefab;

    private Transform arrowparent;

    Vector3 arrowpos;



    private float arrowx=0f, arrowy = 2.5f, arrowz = 2.5f;

    private float  arrow_cooldown = 0.4f;
    

    private float rightoffsetX = 1.5f, leftoffsetX = -1.5f, offsetY, offsetZ = 2f;


    private void Awake()
    {
        
        health = Random.Range(minHealth, maxHealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        clonePositionControler = GameObject.FindGameObjectWithTag("Player").GetComponent<ClonePositionControler>();
        
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
    }
    private void Update()
    {
        if (healthText){
        healthText.text = health.ToString();
        }
        if (gameObject.tag == "PlayerClone"){
            transform.position = clonePositionControler.cloneBehaviour[cloneNo].Cube.transform.position;

            
        }

        Debug.Log(transform.position - new Vector3(0, 0, 1f));

    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Weapon")
        {

            health--;
            if (health == 0)
            {
                if (gameObject.tag == "EnemyDummy")
                {
                    Destroy(gameObject);
                }
                else if (gameObject.tag == "EnemyPlayer")
                {
                    Destroy(healthText);
                    
                    transform.SetParent(player, true);
                    transform.tag = "PlayerClone";


                    for(int i = 0; i <clonePositionControler.cloneBehaviour.Count; i++)
                    {
                        if(!clonePositionControler.cloneBehaviour[i].IsFull)
                        {
                            
                            transform.position = clonePositionControler.cloneBehaviour[i].Cube.transform.position;
                            clonePositionControler.cloneBehaviour[i].IsFull = true;
                            cloneNo = i;
                            break;
                        }
                    }
                    
                    StartCoroutine(constantShoot());

                    
                    


                }
            }
        }
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
        if (gameObject.tag == "PlayerClone" && target.gameObject.tag == "EnemyPlayer")
        {
            clonePositionControler.cloneBehaviour[cloneNo].IsFull = false;
            Destroy(gameObject);
            
        }

        if (gameObject.tag == "EnemyPlayer" && target.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            // level bitme scripti yaz
        }

        
    }

}

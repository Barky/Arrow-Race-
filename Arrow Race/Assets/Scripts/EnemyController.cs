using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    ClonePositionControler clonePositionControler;
    private TextMeshPro healthText;
    private int minHealth = 2, maxHealth = 6, health;
    private Transform player;
    

    private float rightoffsetX = 1.5f, leftoffsetX = -1.5f, offsetY, offsetZ = 2f;

    private int rightxno=0, leftxno=0, backno=0;

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
            transform.position = clonePositionControler.cloneBehaviour[0].Cube.transform.position;
        }

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
                    transform.position = clonePositionControler.cloneBehaviour[0].Cube.transform.position;
                    
                    


                }
            }
        }
    }
    
    private void OnCollisionEnter(Collision target)
    {
        
        if (gameObject.tag == "EnemyPlayer" && target.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            // level bitme scripti yaz
        }
    }
}

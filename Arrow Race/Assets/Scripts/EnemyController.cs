using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{

    private TextMeshPro healthText;
    private int minHealth = 2, maxHealth = 6, health;
    [SerializeField] private Transform playerprefab;
    private Transform player;


    private void Awake()
    {
        health = Random.Range(minHealth, maxHealth);
        player = GameObject.FindGameObjectWithTag("Player").transform;

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

        healthText.text = health.ToString();
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
                    Destroy(gameObject);
                    // eðer enemyplayer ise yanýna katýlma scripti yaz
                    Vector3 newposition = new Vector3(player.transform.position.x + Random.Range(-2f, 2f), player.transform.position.y, player.transform.position.z);
                    Transform newplayer = (Transform)Instantiate(playerprefab, newposition, Quaternion.identity);

                }
            }
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            // level bitme scripti yaz
        }
    }
}

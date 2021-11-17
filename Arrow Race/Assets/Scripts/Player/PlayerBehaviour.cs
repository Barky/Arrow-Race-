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

    private float  arrow_cooldown = 0.4f;

    private void Awake()
    {
       // _GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        arrowpos = new Vector3(transform.position.x + arrowx, transform.position.y + arrowy, transform.position.z + arrowz);
        arrowparent = transform;

    }
    private void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "PlayerClone"){
        StartCoroutine(constantShoot());
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
        
    }}
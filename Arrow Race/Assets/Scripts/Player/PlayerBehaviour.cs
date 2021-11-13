using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    bool isShootingReady = true;
    [SerializeField]
    private Transform RockPrefab;

    //private Transform RockParent;

    private Transform rockPos;

    [SerializeField]
    private float rockforce = 10f, rock_cooldown = 2f;

    private void Awake()
    {
        rockPos = GameObject.Find("hand.R").transform;
    }
    private void Start()
    {
        StartCoroutine(constantShoot());
    }

    public void Shoot()
    {
        Vector3 rockposition = rockPos.transform.position;

        //if you want to arrange the position, change these parameters
        //rockposition.x += 1f;
        //rockposition.y += 1f;
        //rockposition.z += 1f;

        Transform newRock = (Transform)Instantiate(RockPrefab, rockposition, Quaternion.identity);
        newRock.GetComponent<Rigidbody>().AddForce(transform.forward * rockforce);
        //newRock.parent = transform;


    }

    IEnumerator constantShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(rock_cooldown);
        }
    }
}

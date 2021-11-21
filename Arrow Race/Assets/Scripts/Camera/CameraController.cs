using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float offsetx = 0f;
     private float constanty = 13.2f;
    private float offsetz = -9.5f;
     private float cameralerptime = 0.03f;
     bool temp = true;

    private Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

       // transform.Rotate(40f, 0f, 0f, Space.Self);
        transform.position = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);
    }

    private void Update()
    {
        if (Player){
        if(GameManager.instance.LevelEndGame && temp){
            transform.position = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty - 6.5f, Player.transform.position.z + offsetz+3.5f);
            transform.Rotate(-6.6f, 0f, 0f, Space.Self);
            temp = false;
            return;
        }
        Vector3 targetposition = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);

        transform.position = Vector3.Lerp(transform.position, targetposition, cameralerptime);

        
    }
    }
}
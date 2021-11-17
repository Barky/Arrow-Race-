using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float offsetx = 1.6f;
     private float constanty = 12.26f;
    private float offsetz = -8.67f;
     private float cameralerptime = 0.03f;

    private Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

       // transform.Rotate(40f, 0f, 0f, Space.Self);
        transform.position = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);
    }

    private void Update()
    {
        Vector3 targetposition = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);

        transform.position = Vector3.Lerp(transform.position, targetposition, cameralerptime);
    }
}

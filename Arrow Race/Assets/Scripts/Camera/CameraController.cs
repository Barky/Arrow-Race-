using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CameraController : MonoBehaviour
{
    private float offsetx = 0f;
     private float constanty = 13.2f;
    private float offsetz = -9.5f;
     private float cameralerptime = 0.03f;
     bool temp = true;
    private Vector3 newpositioncamera;
    private float duration= 0.1f, delay=0.2f;
    private Transform Player;

    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        // transform.Rotate(40f, 0f, 0f, Space.Self);
        transform.position = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);
        if (Player){
        if(GameManager.instance.LevelEndGame && temp){
            newpositioncamera = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty - 6.5f, Player.transform.position.z + offsetz + 3.5f);
               StartCoroutine(camerachange());
                
                //transform.position = newpositioncamera;
               
            
           // transform.Rotate(-6.6f, 0f, 0f, Space.Self);
            temp = false;
            return;
        }
        newpositioncamera = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);

            Tween.Position(transform, newpositioncamera, duration, delay);
            //transform.position = Vector3.Lerp(transform.position, newpositioncamera, cameralerptime);

        
    }
    }

    IEnumerator camerachange()
    {
        yield return new WaitForSeconds(1f);
        Tween.Rotate(transform, new Vector3(-6.6f, 0f, 0f), Space.Self, 0.01f, 0.01f);
        Tween.Position(transform, newpositioncamera, 0.01f, 0.01f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CameraController : MonoBehaviour
{
    private float offsetx = 0f;
     private float constanty = 13.2f;
    private float offsetz = -9.5f;
     bool temp = true;
    private Vector3 newpositioncamera;
    private float duration= 0.1f, delay=0.2f;
    private Transform Player;

    private void Update()
    {
        if (!Player)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }
            switch (GameManager.instance.LevelEndGame)
            {
                case true:
                    newpositioncamera = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + 9.5f, Player.transform.position.z + offsetz + 0.8f);
                    break;
                case false:
                    newpositioncamera = new Vector3(Player.transform.position.x + offsetx, Player.transform.position.y + constanty, Player.transform.position.z + offsetz);
                    break;
            }
            Tween.Position(transform, newpositioncamera, duration, delay);
    
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleController : MonoBehaviour
{
    public Transform shotFX;
    private Transform player;
    private Transform FXParent;

    //private void OnCollisionEnter(Collision target)
    //{
    //    if(target.gameObject.tag == "Player")
    //    {
    //        GameManager.instance.playerDied = true;
    //    }
    //    else if (target.gameObject.tag == "PlayerClone")
    //    {
    //        Destroy()
    //    }
    //}
    private void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Weapon")
        {
            StartCoroutine(shotfx());
        }
        }
    
    IEnumerator shotfx()
    {
        Transform fxcreated;
        Vector3 fxpos = transform.position;
        fxpos.z -= 1.5f;
        fxcreated = Instantiate(shotFX, fxpos, Quaternion.identity);
        fxcreated.name = "shotFX";
        fxcreated.parent = FXParent;
        yield return new WaitForSeconds(1f);
        Destroy(fxcreated.gameObject);
    }
}

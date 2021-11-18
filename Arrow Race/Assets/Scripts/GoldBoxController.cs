using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Weapon")
        {
                Destroy(gameObject);
                // gold ++;
        }
    }
}

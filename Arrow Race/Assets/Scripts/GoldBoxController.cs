using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    private int goldHealth = 2;
    private int totalgold;


    private void OnTriggerEnter(Collider target) {
        if(target.gameObject.tag == "Weapon")
        {
            Debug.Log("çarptı " + goldHealth.ToString());
            goldHealth --;
            
            if (goldHealth == 0)
            {

                Destroy(gameObject);
                totalgold ++;
                }
                
        }
    }
}

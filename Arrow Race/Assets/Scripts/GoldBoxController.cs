using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    private int goldHealth = 2;
    


    private void OnTriggerEnter(Collider target) {
        if(target.gameObject.tag == "Weapon")
        {
            goldHealth --;
            
            if (goldHealth == 0)
            {
                GameManager.instance.IncrementGold();
                Destroy(gameObject);
               
                }
                
        }
    }
}

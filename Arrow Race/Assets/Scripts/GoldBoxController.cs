using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBoxController : MonoBehaviour
{
    private int goldHealth = 2;
    public Transform fxprefab;
    private Transform FXParent;
    IEnumerator goldfx()
    {
        Transform fxcreated;
        Vector3 fxpos = transform.position;
        fxpos.z -= 0.5f;
        fxcreated = Instantiate(fxprefab, fxpos, Quaternion.identity);
        fxcreated.name = "goldfx";
        fxcreated.parent = FXParent;
        yield return new WaitForSeconds(1f);
        Destroy(fxcreated.gameObject);
    }
    private void Start()
    {
        FXParent = GameObject.Find("/Particles").transform;
    }
    private void OnTriggerEnter(Collider target) {
        if(target.gameObject.tag == "Weapon")
        {
            goldHealth --;
            StartCoroutine(goldfx());
            
            if (goldHealth == 0)
            {
               GameplayManager.instance.IncrementGold();
                Destroy(gameObject);
               
                }
                
        }
    }
}

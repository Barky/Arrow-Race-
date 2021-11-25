using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    
    private float lifetime =100f;
    private float speed = 15f;
    private float startX, startY, startZ;

    private void Awake()
    {
        transform.Rotate(0f, 90f, 0f, Space.Self);
        startX = transform.position.x;
        startY = transform.position.y;
        startZ = transform.position.z;
    }
    private void Start()
    {
        StartCoroutine(DestroyArrow());
    }
    private void Update()
    {
       transform.position = new Vector3 (startX, startY, transform.position.z + speed * Time.deltaTime);
        
        if (transform.position.z - startZ > 50f ||  GameManager.instance.playerDied) // ka� birim sonra yok olaca��n� belirle
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyArrow()
    {
        
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider target)//ba�ka bir objenin collider � arrowun trigger�na �arparsa
    {
        if (target.tag == "EnemyDummy" || target.tag == "EnemyPlayer" || target.tag == "Obstacle" || target.tag == "GoldBox" )
        {
            Destroy(gameObject);
        }

    }
}

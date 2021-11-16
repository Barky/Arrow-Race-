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

        if (transform.position.z - startZ > 50f) // kaç birim sonra yok olacaðýný belirle
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider target)//baþka bir objenin collider ý arrowun triggerýna çarparsa
    {
        if (target.tag == "EnemyDummy" || target.tag == "EnemyPlayer" || target.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}

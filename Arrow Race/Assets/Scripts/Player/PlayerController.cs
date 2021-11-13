using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    private float swerveSpeed = 20f, platformWidth = 5f, movementSpeed = 5f;

    Vector3 movementPosition;
    private void Update()
    {
        float newx = 0, swipeDelta = 0;

        // if on mobile
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            swipeDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }

        //if on unity editor
        else if (Input.GetMouseButton(0))
        {
            swipeDelta = Input.GetAxis("Mouse X");
        }

        newx = transform.position.x + swipeDelta * swerveSpeed *Time.deltaTime;


        newx = Mathf.Clamp(newx, -platformWidth, platformWidth);
        movementPosition = new Vector3(newx, transform.position.y, transform.position.z + movementSpeed * Time.deltaTime);
        transform.position = movementPosition;
    }
}

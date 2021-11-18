using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    private float swerveSpeed = 400f, platformWidth = 5.5f, movementSpeed = 5f;
    private bool isGameStarted;
    Vector3 movementPosition, firstposition;
    private Transform lastposition;
    Slider levelslider;

    //public void sliderchange()
    //{
    //    float distances = lastposition.transform.position.z;
    //    levelslider.value = transform.position.z / distances;

    //}
    private void Awake()
    {
        levelslider = GameObject.Find("/UICamera/Canvas/in_level_panel/level_bar").GetComponent<Slider>();
        firstposition = transform.position;
        lastposition = GameObject.Find("/Platform/LevelEnd").transform;
        
    }
    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            levelslider.value = 6f;
            StartCoroutine(sliderchange());
        }
    }

    IEnumerator sliderchange()
    {
        
        while (levelslider.value < 100)
        {
            levelslider.value += 2f;
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
    private void Update()
    {
        // sliderchange();
        
        
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

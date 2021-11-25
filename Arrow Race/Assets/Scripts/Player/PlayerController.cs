using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    private float swerveSpeed = 1300f, platformWidth = 5.5f, movementSpeed = 4f, sliderchange, transformlength;
    private bool isGameStarted;
    private float firstposition,lastpositionz, sliderchangez;
    private Transform lastposition;
    private Vector3 movementPosition;
    Slider levelslider;
    Animator anim;

    //public void sliderchange()
    //{
    //    float distances = lastposition.transform.position.z;
    //    levelslider.value = transform.position.z / distances;

    //}
    private void Awake()
    {
        levelslider = GameObject.Find("/UICamera/Canvas/in_level_panel/level_bar").GetComponent<Slider>();
        firstposition = transform.position.z;
        Transform levelend = GameObject.Find("/LevelEnd").transform;
        lastpositionz = levelend.transform.position.z;
        anim = GetComponent<Animator>();
        
    }
    private void Start()
    {

        levelslider.value = 0.2f;
         transformlength = lastpositionz - firstposition;
        Debug.Log(transformlength);
      //   StartCoroutine(onesecdistance());

    }
    void slidermove(float distancemade){
        if (distancemade > 0){

        
        }
    }
    private void Update()
    {
        if (GameManager.instance.levelFinished){
            return;
        }
        
        if(!GameManager.instance.LevelStarted){
            return;
        }
        anim.SetBool("gameStarted", true);
        float newx = 0, swipeDelta = 0;
        // if on mobile
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            swipeDelta = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }

        //if on unity editor
        else if (Input.GetMouseButton(0))
        {
            swipeDelta = Input.GetAxis("Mouse X");
        }
        newx = transform.position.x + swipeDelta * swerveSpeed*Time.deltaTime;


        newx = Mathf.Clamp(newx, -platformWidth, platformWidth);
        if(GameManager.instance.LevelEndGame){
            movementPosition = new Vector3(newx, transform.position.y, transform.position.z);
            transform.position = movementPosition;
            return;
        }

        movementPosition = new Vector3(newx, transform.position.y, transform.position.z + movementSpeed * Time.deltaTime);
        sliderchangez = ((movementPosition.z - transform.position.z) * 1.8f) / transformlength;
        levelslider.value += sliderchangez;
        transform.position = movementPosition;
        
        
    
    }
}

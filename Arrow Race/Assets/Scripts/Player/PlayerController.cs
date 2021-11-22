using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    private float swerveSpeed = 1300f, platformWidth = 5.5f, movementSpeed = 4f, sliderchange, transformlength;
    private bool isGameStarted;
    Vector3 movementPosition, firstposition;
    private Transform lastposition;
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
        firstposition = transform.position;
        lastposition = GameObject.Find("/LevelEnd").transform;
        
        anim = GetComponent<Animator>();
        
    }
    private void Start()
    {
    
         if (gameObject.tag == "Player")
         {
             levelslider.value = 6f;
            //  StartCoroutine(sliderchange());
         }
         transformlength = lastposition.position.z - firstposition.z;
         StartCoroutine(onesecdistance());

    }
    void slidermove(float distancemade){
        if (distancemade > 0){

        
        }
    }
    // IEnumerator sliderchange()
    // {
        
    //     while (levelslider.value < 100)
    //     {
    //         levelslider.value += 2f;
    //         yield return new WaitForSeconds(1f);
    //     }
    //     yield return null;
    // }
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
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
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
        // slidermove((movementPosition.z - transform.position.z));
        transform.position = movementPosition;
        
        
    
    }
    IEnumerator onesecdistance()
    {
        float firstpos, lastpos, onesecdist;
        while(true){
        if ( GameManager.instance.LevelStarted && !GameManager.instance.LevelEndGame)
        {
         firstpos = transform.position.z;
        yield return new WaitForSeconds(2f);
        lastpos = transform.position.z;
        onesecdist = lastpos - firstpos;
       // Debug.Log("one sec dist: "+onesecdist+"transformlegnth: "+transformlength+ "sliderch: "+94f * onesecdist / transformlength);
        sliderchange += (94f * onesecdist / transformlength);
        levelslider.value += sliderchange;
        }
        yield return null;
        }
    }
}

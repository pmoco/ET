using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


public enum FlashState
{
    Reloading,
    Standby,
    Flashing
}

    
    public float movSpeed;
    float  speedX, speedY;
    Vector2 movement; 
    bool isActive = true ;

    
    public FlashState flashState = FlashState.Standby;

    float flashTimer; 

    public float flashAnimationTime ;

    public GameObject flash; 
    
 public float FLASHLIGHT_OFFSET =90f;

    Rigidbody2D rb; 


    
        // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        flashState = FlashState.Standby;
    }


    // Update is called once per frame
    void Update()
    {
        

         speedX = Input.GetAxisRaw("Horizontal");   

         speedY = Input.GetAxisRaw("Vertical");

            

        if (Input.GetMouseButtonDown(0) &&  flashState == FlashState.Standby){
            flashTimer = 0; 
            flashState = FlashState.Flashing;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; 
            Point (mousePosition);

        }else if(flashState== FlashState.Flashing ){

           Flash ();

        }
        


    }

    
    public void TakePhotoOf ( Transform  target){
        flashTimer = 0; 
        flashState = FlashState.Flashing;
        Point(target.position);
    }


    void Point (Vector3 target ){

            

            Vector3 direction = target - flash.transform.position;

            // Calculate the angle between the direction and the right vector (1,0)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg   +FLASHLIGHT_OFFSET;

            // Rotate the object to face the mouse position
            flash.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (isActive ){

           

            movement =  new Vector2(speedX, speedY);


            rb.velocity = movement.normalized * movSpeed;
        }
    }


    void Flash ( ){

        flashTimer+= Time.deltaTime ;


        if (flashTimer < flashAnimationTime ){
             flash.SetActive(true);


        }else{
            flashState = FlashState.Standby ; 
            flash.SetActive(false);

        }

    }

   
    public void toggleActive(){
        if (isActive){
            isActive=false;
        }else{

            isActive=true;
        }

    }
}

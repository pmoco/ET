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

  
    public float flashAnimationTime ;

    public GameObject flash; 
    
    public float FLASHLIGHT_OFFSET =90f;

    public float reloadTime = 2f;

    public float flashTimer;

    private AudioSource audioSource;

    Rigidbody2D rb;

    Animator anim; 

    
        // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

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
        if (flashState== FlashState.Reloading)
        {
            Reloading ();
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



            audioSource = GetComponent<AudioSource>();

            audioSource.Play();



            if (speedY > 0)
            {

                anim.SetBool("movUp", true);
                anim.SetBool("movDown", false);
            }
            else if (speedY < 0) 
            {
                anim.SetBool("movUp", false);
                anim.SetBool("movDown", true);

            }else if (speedX < 0)
            {
                anim.SetBool("movUp", false);
                anim.SetBool("movDown", false);
                anim.SetBool("movLeft", true);
                anim.SetBool("movRight", false);
            }
            else if (speedX > 0)
            {
                anim.SetBool("movUp", false);
                anim.SetBool("movDown", false);
                anim.SetBool("movLeft", false);
                anim.SetBool("movRight", true);
            }
            else
            {
                anim.SetBool("movUp", false);
                anim.SetBool("movDown", false);
                anim.SetBool("movLeft", false);
                anim.SetBool("movRight", false);
            }


        }
    }

    void Reloading()
    {
        flashTimer += Time.deltaTime;
        if (flashTimer >= reloadTime)
        {
            flashTimer = 0;
            flashState = FlashState.Standby;
        }
    }



    void Flash ( ){

        flashTimer+= Time.deltaTime ;


        if (flashTimer < flashAnimationTime ){

             flash.SetActive(true);

            audioSource = GetComponent<AudioSource>();

            audioSource.Play();

        }
        else{
            flashTimer = 0;
            flashState = FlashState.Reloading ; 
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


    public static float MapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        // Ensure the input value is within the input range
        value = Mathf.Clamp(value, inputMin, inputMax);

        // Map the input value from the input range to the output range
        return outputMin + (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
    }
}

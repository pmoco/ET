using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Rigidbody2D rb;

    public bool _isOpen;
    bool _isClosing; 
    public int hp; 
    HingeJoint2D joint;
    float _meanAngle; 

    public Vector3 initialPos;
    Quaternion neutralRotation; 

    public Collider2D[] colliderToIgnore;

    public float closeSpeed = 100f;


    public Transform rotationAnchor; 
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();

        initialPos = this.transform.position ;
        neutralRotation =  this.transform.rotation ;

        foreach(Collider2D col in colliderToIgnore){
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), col);
        }

        float lowerAngleLimit = joint.limits.min;
        float upperAngleLimit = joint.limits.max;

        _meanAngle = (lowerAngleLimit + upperAngleLimit) / 2f;
    }

    // Update is called once per frame
    void Update()
    {
         if(_isClosing){
            Close();
        }


    }


    public void Open(){

        if (!_isOpen){
            _isOpen =true;
            joint.enabled = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Open");

            
        }


    }

    public void Close(){

    float  angle = joint.jointAngle;

        if (angle > _meanAngle+ 0.01 || angle < _meanAngle -0.01)
        {
            _isClosing = true;
            
            float rotationAngle ;
            
            if (angle < _meanAngle){
               rotationAngle =  closeSpeed * Time.deltaTime * -1; 

            }else {
                rotationAngle =  closeSpeed * Time.deltaTime ; 
            }

            
            
            Vector3 directionToRotationPoint = rotationAnchor.position - transform.position;
        
                // Calculate the rotation angle based on the direction and speed

            transform.RotateAround(rotationAnchor.position, Vector3.forward, rotationAngle);

            
            // this.transform.position = Vector3.MoveTowards(transform.position , initialPos,  closeSpeed * Time.deltaTime);
            // this.transform.rotation = neutralRotation;
            
            Debug.Log("Closign " +  angle);
        }else{
            _isOpen = false;
            _isClosing = false;

            joint.enabled = false; 
            rb.bodyType = RigidbodyType2D.Static;
             Debug.Log("Close");
        }
    }
}

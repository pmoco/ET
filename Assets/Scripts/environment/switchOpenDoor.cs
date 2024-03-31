using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchOpenDoor : MonoBehaviour
{
    [SerializeField] DoorController[]  doorControllers ; 
    
    [SerializeField] bool _isDoorOpen; 

    bool isPlayerInside; 


    [SerializeField] InventoryManager.AllItems requiredItem ;

    //le door sound (Cavaleiro)
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (isPlayerInside && Input.GetKeyDown("e"))
        {
            if (_isDoorOpen){

                 foreach(DoorController door in doorControllers ){

                    door.Close();

                    audioSource = GetComponent<AudioSource>();

                    audioSource.Play();
                }
                _isDoorOpen =false;
            }else{

                if (InventoryManager.Instance.hasItem(requiredItem)){
                    foreach(DoorController door in doorControllers ){

                        door.Open();

                        audioSource = GetComponent<AudioSource>();

                        audioSource.Play();
                    }
                    _isDoorOpen =true;
                }else{
                    this.GetComponent<PopupText>().PopUpTimeout(0);
                    
                }
                

            }
           
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger area
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

}

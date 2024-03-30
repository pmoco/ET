using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class switchOpenDoor : MonoBehaviour
{
    [SerializeField] DoorController[]  doorControllers ; 
    
    [SerializeField] bool _isDoorOpen; 

    bool isPlayerInside; 


    [SerializeField] InventoryManager.AllItems requiredItem ;


    
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

                }
                _isDoorOpen =false;
            }else{

                if (InventoryManager.Instance.hasItem(requiredItem)){
                    foreach(DoorController door in doorControllers ){

                        door.Open();

                    }
                    _isDoorOpen =true;
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

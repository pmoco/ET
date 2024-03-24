using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems itemType ;

    public Sprite bigSprite; 

    public string ItemName ; 
    public string description;

    

    public bool isPlayerInside; 


    public bool  Add(){

        return InventoryManager.Instance.AddItem(itemType);
    
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the player enters the trigger area
            if (collision.CompareTag("Player"))
            {
                isPlayerInside = true;
            }
        }

    
    protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            // Check if the player exits the trigger area
            if (collision.CompareTag("Player"))
            {
                isPlayerInside = false;
            }
        }



}

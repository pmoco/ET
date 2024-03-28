using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   
    public static InventoryManager Instance ; 

    public List<AllItems> _inventoryItems =  new List<AllItems>();  // Picked up Items 



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Instance =  this; 

    }

    public bool AddItem (AllItems item){

        if ( _inventoryItems.Contains(item) ) // if item exists  just return false
        {
            return false;
        }else{
            // if the item isnt in the inventory, Add Item ,  return true 
            _inventoryItems.Add(item);

            UIManager.Instance.UpdateBackpack(_inventoryItems);


            return true;
        }


    }


    public  enum AllItems //ALL items in game 
    {
        KeyCard, 

        OfficeKey,
        
        Camera

    }


    public bool hasItem(AllItems item) {
        
        return Instance._inventoryItems.Contains(item); 

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
   
    public static InventoryManager Instance ;


    public  List<AllItems> _inventoryItems =  new List<AllItems>();  // Picked up Items 
    public List<AllItems> _prevItems = new List<AllItems>();


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // Ensure only one instance of DataHolder exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);


            UpdateAfromB(_prevItems, _inventoryItems);



        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem (AllItems item, bool toBackPack =  false){

        if ( _inventoryItems.Contains(item) ) // if item exists  just return false
        {
            return false;
        }else{
            // if the item isnt in the inventory, Add Item ,  return true 
            _inventoryItems.Add(item);

            if (toBackPack)
            {
                UIManager.Instance.UpdateBackpack(item);
            }
            


            return true;
        }


    }


    public  enum AllItems //ALL items in game 
    {
        KeyCard, 

        OfficeKey,
        
        Camera,

        jujuBeads, 
        None

    }


    public bool hasItem(AllItems item) {
        
        return Instance._inventoryItems.Contains(item); 

    }


    public void FailedRun()
    {

        UpdateAfromB( _inventoryItems, _prevItems);

    }


    public void SuccessRun()
    {

         UpdateAfromB(_prevItems, _inventoryItems);


    }



    public void UpdateAfromB (List<AllItems > A ,  List<AllItems> B)
    {

        A.Clear();

        foreach (AllItems i in B)
        {

            A.Add(i);
        }


    }

}

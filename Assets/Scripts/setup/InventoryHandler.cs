using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;
using System.Linq;
using TMPro; // Import LINQ namespace


public class InventoryHandler : MonoBehaviour
{
    public static InventoryHandler Instance ;


    public List<AllItems> _inventoryItems;

    public TMP_Text attempt;

    [Serializable]
    public struct ItemTuple
    {
        public AllItems item;
        public GameObject gameObject;
    }


        public List<ItemTuple> connections = new List<ItemTuple>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public int currentItem =1;

    public int totalItems;

    public TMP_Text pageLabel;

    public bool isLive = true;

    public GameObject Wraper; 



    void Awake()
    {
        // Ensure only one instance of DataHolder exists
        if (Instance == null)
        {
            Instance = this;
        }


    }


    private void Start()
    {

        attempt.SetText("Attempt #" + GameManager.Instance.attempt);
        _inventoryItems = InventoryManager.Instance._inventoryItems;
        Show();
    }
    


    void UpdatePageNumber(int pageNumber, int total)
    {

        pageLabel.SetText((pageNumber+1) + "/" + total);

    }


   public  void nextItem()
    {

        GameObject current = FindGameObjectByItem(_inventoryItems[currentItem] );
        current.SetActive(false);
        currentItem++;

        if (currentItem == totalItems){
            currentItem = 0;
        }

        current = FindGameObjectByItem(_inventoryItems[currentItem]);

        if (current != null)
        {
            current.SetActive(true);
        }


    }

    public void prevItem()
    {

        GameObject current = FindGameObjectByItem(_inventoryItems[currentItem]);
        current.SetActive(false);
        currentItem--;

        if (currentItem < 0)
        {
            currentItem = totalItems-1;
        }

        current = FindGameObjectByItem(_inventoryItems[currentItem]);

        if (current != null)
        {
            current.SetActive(true);
        }

    }

    public void Show()
    {
        totalItems = _inventoryItems.Count;
        currentItem = 0;

        UpdatePageNumber(currentItem,totalItems);
       

        Wraper.SetActive(true);

    }
    public void Hide()
    {
        Wraper.SetActive(false);

    }






    public GameObject FindGameObjectByItem(AllItems item)
    {
        // Use LINQ to search for the tuple with matching AllItems enum
        ItemTuple foundTuple = connections.FirstOrDefault(t => t.item == item);

        // If a tuple with the matching enum is found, return the associated GameObject
        if (foundTuple.item != AllItems.None) // Assuming Sword is a default enum value
        {
            return foundTuple.gameObject;
        }
        else
        {
            Debug.LogWarning("GameObject for " + item + " not found.");
            return null;
        }
    }

}

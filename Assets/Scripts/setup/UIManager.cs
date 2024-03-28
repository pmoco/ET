using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InventoryManager;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public ConsoleLogger console;

    public GameObject backPack;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Instance.console= GetComponentInChildren<ConsoleLogger>();
        Instance.backPack = backPack;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateBackpack(List<AllItems> inventory)

    {
        string text = "Backpack : <br>";

        foreach (AllItems item in inventory)
        {
            text += " _ "+item.ToString() +  "<br>";

        }

        Debug.LogWarning(text);



        backPack.GetComponent<TMP_Text>().SetText( text);   
    }
}

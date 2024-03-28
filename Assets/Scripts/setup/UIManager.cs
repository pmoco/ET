using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using static InventoryManager;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public ConsoleLogger console;

    public GameObject backPack;
    public GameObject deathScreen;
    public GameObject runScreen;








    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        Instance.console = console;
        Instance.backPack = backPack;
        Instance.deathScreen = deathScreen;
        Instance.runScreen = runScreen; 


        backPack.GetComponent<TMP_Text>().SetText(GameManager.Instance.BackPackContent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ConsoleShow(string text)
    {

        console.Show(text);
    }

    public void UpdateBackpack(AllItems item)

    {
        string text = backPack.GetComponent<TMP_Text>().text;

        text += "<br>   _ " + item.ToString() +  "<br>";


        Debug.LogWarning(text);



        backPack.GetComponent<TMP_Text>().SetText( text);   
    }


    public void DeathScreen()
    {

        deathScreen.SetActive(true);
    }

    public void SuccessScreen()
    {

        runScreen.SetActive(true);
    }

}

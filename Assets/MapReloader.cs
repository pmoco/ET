using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReloader : MonoBehaviour
{
    public static MapReloader Instance; 

    void Awake()
    {
        // Ensure only one instance of DataHolder exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

               }
        else
        {
            Destroy(gameObject);
        }
    }

 
    public void  Hide()
    {
        gameObject.SetActive(false);

    }


    public void Show()
    {
        gameObject.SetActive(true);
    }


}

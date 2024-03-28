using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager  i ; 

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        i =  this;
    }


    // public static GameManager i {
        
    //     get{
    //         if (i==null ) _i = Instantiate(Resources.Load<GameManager>("GameManager"));
    //         return _i;
    //     }
    // }


    public GameObject PopUpPrefab; 


}

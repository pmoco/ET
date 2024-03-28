using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{

    public static GameManager  Instance ;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    public int attempt = 1;

    public bool inRun = false;

    public float GameTimer = 0;

    public float warning1 = 0;

    public float warningMayhem = 0;

    public string BackPackContent = "BackPack <br>   _ Camera";

    public enum GameState
    {
        Menu,
        Early,
        Mid,
        Late,
    }

    public GameState State = GameState.Menu;



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
    public GameObject PopUpPrefab;

    public void DeathScreen()
    {
        UIManager.Instance.DeathScreen();
        InventoryManager.Instance.FailedRun();

    }

    public void SuccessScreen()
    {
        UIManager.Instance.SuccessScreen();
        InventoryManager.Instance.SuccessRun();
    }




    private void Update()
    {
        if (inRun)
        {
            GameTimer += Time.deltaTime;

            if (GameTimer < warning1)
            {
                StartPhase2();

            }else if(GameTimer > warning1 && GameTimer < warningMayhem){
                StartMayhem();
            }





        }

    }


    public void StartGame() { 



        State = GameState.Early;
        SceneManager.LoadScene("SampleScene");

        
    }




    public void StartPhase2()
    {
        if (State == GameState.Early)
        {
            State = GameState.Mid;
        }
    }

    public void StartMayhem()
    {
        if (State == GameState.Mid)
        {
            State = GameState.Late;
        }
    }

    public void BackToMenu()
    {
        BackPackContent = UIManager.Instance.backPack.GetComponent<TMP_Text>().text;

        State = GameState.Menu;
        SceneManager.LoadScene("Menu");


    }


}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{

    public static GameManager  Instance ;

    private MusicManager musicManager; // Reference to the MusicManager component

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the MusicManager component
        musicManager = FindObjectOfType<MusicManager>();
    }

    // Method to play the next track
    public void PlayNextTrack()
    {
        if (musicManager != null)
        {
            // Determine the index of the next track
            int nextTrackIndex = (musicManager.currentTrackIndex + 1) % musicManager.musicTracks.Length;
            
            // Play the next track
            musicManager.PlayMusicTrack(nextTrackIndex);
        }
        else
        {
            Debug.LogWarning("MusicManager not found!");
        }
    }

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


    public float SpawnIntensity1  = 10f ;

    public float SpawnIntensity2 = 8f;

    public float MaxSpawnIntensity = 3f;

    public float TimeToMaxSpawn = 60f;

    public SpawnerControler spawner ;

    void Awake()
    {
        // Ensure only one instance of DataHolder exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Get a reference to the MusicManager component
            musicManager = FindObjectOfType<MusicManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject PopUpPrefab;

    public void DeathScreen()
    {
        inRun = false;

        UIManager.Instance.DeathScreen();
        InventoryManager.Instance.FailedRun();

    }

    public void SuccessScreen()
    {
        inRun = false;
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
                // do nothing

            }else if(GameTimer > warning1 && GameTimer < warningMayhem){
                StartPhase2();



            }
            else if  (GameTimer > warningMayhem)
            {
                StartMayhem();
            }





        }

    }



    public void StartGame() { 


        SceneManager.LoadScene("SampleScene");

        if (State == GameState.Menu)
        {
            State = GameState.Early;

            inRun = true;
        }

    }




    public void StartPhase2()
    {
        if (State == GameState.Early)
        {
            State = GameState.Mid;

            spawner = SpawnerControler.Instance;

            spawner.isSpawning = true;

            spawner.spawnTimer = SpawnIntensity1; 


        }
    }

    public void StartMayhem()
    {
        if (State == GameState.Mid)
        {
            State = GameState.Late;
        }

        float t = Mathf.Clamp01(GameTimer- warningMayhem / TimeToMaxSpawn);

        // Map the interpolation factor to interpolate between startValue and endValue
        float currentValue = Mathf.Lerp(SpawnIntensity2, MaxSpawnIntensity, t);


        spawner.spawnTimer = currentValue;
    }

    public void BackToMenu()
    {
        BackPackContent = UIManager.Instance.backPack.GetComponent<TMP_Text>().text;

        attempt++;

        State = GameState.Menu;
        SceneManager.LoadScene("Menu");


    }



}

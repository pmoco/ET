using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpawnerControler : MonoBehaviour
{


    public static SpawnerControler Instance;
    public GameObject enemyPrefab;


    public bool isSpawning = false;

    public List<Transform> spawners ;


    public float spawnTimer = 3f;

    float timer = 0;



    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    private void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        // Add each child Transform to the list
        foreach (Transform child in children)
        {
            if (!spawners.Contains(child) && !transform.Equals (child))
            {
                spawners.Add(child);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (isSpawning)
        {
            timer += Time.deltaTime;

            if (timer > spawnTimer)
            {
                timer = 0;

                SpawnEnemy();
            }

        }


    }



    public void SpawnEnemy()
    {

        int randomIndex = Random.Range(0, spawners.Count);
        Transform spawnPoint = spawners[randomIndex];

        // Spawn an enemy at the chosen spawner
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);


    }
}

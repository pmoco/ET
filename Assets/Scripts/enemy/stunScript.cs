using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunScript : MonoBehaviour
{

    private LayerMask obstacleLayer; // Layer mask for detecting obstacles

    private  AudioSource audioSource;

    private void Start()
    {
        obstacleLayer = LayerMask.GetMask("obstacle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger area
        if (collision.CompareTag("Enemy"))
        {
            
            GameObject enemy =  collision.gameObject;

            if (IsPathClear(enemy)) enemy.GetComponent<EnemyController>().Stun()  ;

            audioSource = GetComponent<AudioSource>();

            audioSource.Play();

        }
    }


    bool IsPathClear(GameObject target)
    {
        // Perform raycast from enemy to player
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, target.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, obstacleLayer);

        // If raycast hits an obstacle, return false
        return (hit.collider == null || hit.collider.CompareTag("Enemy"));
    }

}

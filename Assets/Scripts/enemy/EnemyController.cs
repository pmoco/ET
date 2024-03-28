using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static EnemyController;
public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackCooldown = 2f; // Cooldown between attacks
    private Transform player; // Reference to the player's transform
    private bool canAttack = true; // Flag to control attack cooldown


    public GameObject deathScreen;


    public enum EnemyStatus
    {
        ChasingEnemy,
        StoppingForAttack,
        Attack,
        Stunned
    }


    public EnemyStatus enemyStatus = EnemyStatus.ChasingEnemy;


    AIPath  ai;

    private LayerMask obstacleLayer; // Layer mask for detecting obstacles



    public float shakeIntensity = 0.1f; // Intensity of the shake
    public float shakeDuration = 0.5f; // Duration of the shake
    public float rotationSpeed = 100f; // Speed of rotation
    float shakeTimer = 0f; 
    private Vector3 originalPosition; // Original position of the enemy



    Animator anim;


    public Collider2D attackCollider;

    void Start()
    {
        // Find the player object using a tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ai =  gameObject.GetComponent<AIPath>();
        ai.maxSpeed = maxSpeed;
        anim = GetComponent<Animator>();
        obstacleLayer = LayerMask.GetMask("obstacle");
    }

    void Update()
    {


        if (enemyStatus == EnemyStatus.Stunned)
        {
            Shake();
        }


        if (enemyStatus == EnemyStatus.ChasingEnemy && isPlayerInRange())
        {
            enemyStatus = EnemyStatus.StoppingForAttack;
        }
        if ( (enemyStatus == EnemyStatus.StoppingForAttack) )
        {
            if (isPlayerInRange()) { 
                tryAttacking();
            }
            else
            {
                enemyStatus = EnemyStatus.ChasingEnemy;
                ai.maxSpeed = maxSpeed;
            }
        }
        if (enemyStatus == EnemyStatus.Attack)
        {
            Attack();
            canAttack = false;

            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Attack_done"))
            {
                attackCollider.enabled = true;


            }
            else if (stateInfo.IsName("Movin"))
            {
                attackCollider.enabled = false ;
                Invoke("ResetAttackCooldown", attackCooldown);
            }

                

        }


    }

    bool isPlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) <= attackRange;
    }


    void tryAttacking()
    {


        // Attack the player if cooldown is over
        if (canAttack && IsPathClear())
        {
            ai.maxSpeed = 0f;

            if (ai.velocity.magnitude < 0.15f)
            {
                enemyStatus = EnemyStatus.Attack;
                
            }

        }
    }

    bool IsPathClear()
    {
        // Perform raycast from enemy to player
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance,obstacleLayer);

        // If raycast hits an obstacle, return false
        return (hit.collider == null || hit.collider.CompareTag("Player") );
    }


    public void Stun()
    {
        shakeTimer = 0;
        ai.maxSpeed = 0f;
        originalPosition = transform.position;
        enemyStatus = EnemyStatus.Stunned;
    }



    void Shake()
    {
        shakeTimer += Time.deltaTime;
        if (shakeTimer <= shakeDuration)
        {

            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            transform.position = originalPosition + shakeOffset;

            // Rotate the enemy erratically
            transform.Rotate(Vector3.forward, Random.Range(-rotationSpeed, rotationSpeed) * Time.deltaTime);
        }
        else
        {
            shakeTimer = 0f;
            ai.maxSpeed = 3f;
            enemyStatus = EnemyStatus.ChasingEnemy;
        }
    }





    void Attack()
    {
        anim.SetTrigger("attack");

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);



       
        // Add attack behavior here, such as reducing player health
    }

    void ResetAttackCooldown()
    {
        canAttack = true;
        ai.maxSpeed = maxSpeed;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check if the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            player.movSpeed = 0;

            GameManager.Instance.DeathScreen();
        }
    }


}

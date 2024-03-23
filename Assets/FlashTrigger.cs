using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;


    bool isPlayerInside; 

   public  GameObject player ; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (isPlayerInside && Input.GetKeyDown("e"))
        {
            PlayerController  pc =player.GetComponent<PlayerController>() ; 

            pc.TakePhotoOf (target);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the player enters the trigger area
            if (collision.CompareTag("Player"))
            {
                isPlayerInside = true;
                player =  collision.gameObject;

            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Check if the player exits the trigger area
            if (collision.CompareTag("Player"))
            {
                isPlayerInside = false;
                player =  null;
            }
        }
}

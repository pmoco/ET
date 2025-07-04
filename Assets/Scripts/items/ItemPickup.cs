using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : ItemController 
{

    public AudioSource audioSource; 

    bool toDestroy = false;

    public bool ItemDestroy = true ;
    public bool toBackpack = true ;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource> ();

        if (isOnBackpack()) //Adds the item to the backpack UI and removes it from the map 
        {
            Debug.LogWarning(ItemName + " is On backpack");
            gameObject.SetActive(false);

            if (toBackpack)
            {
                UIManager.Instance.AddToBackpackUI(getItemType());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (toDestroy && !audioSource.isPlaying )
        {
            //Destroy(gameObject);

            gameObject.SetActive ( false );
        }


        if (isPlayerInside && Input.GetKeyDown("e"))
        {   

            audioSource.Play();


            UIManager.Instance.ConsoleShow("Picked Up " + ItemName);

            Add(toBackpack);        
            
            
            
            if (ItemDestroy)
            {
                toDestroy = ItemDestroy;
                gameObject.GetComponent<PopupText>().popOut();
                if (gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }

                
            }
            else
            {
                gameObject.GetComponent<PopupText>().popOut();
                gameObject.GetComponent<FlashTrigger>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.GetComponent<PopupText>().enabled = false;
            }

            

        }


    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        // Check if the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<PopupText>().popUp();
        }
    }

    protected override  void OnTriggerExit2D(Collider2D collision)
    {
         base.OnTriggerExit2D(collision);

        // Check if the player exits the trigger area
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<PopupText>().popOut();
        }
    }

    
}

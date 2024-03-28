using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    // Start is called before the first frame update

    bool isPlayerInside;

    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown("e"))
        {
            GameManager.Instance.SuccessScreen();
        }
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player enters the trigger area
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<PopupText>().popUp();
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits the trigger area
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<PopupText>().popOut();
            isPlayerInside = false;
        }
    }
    }

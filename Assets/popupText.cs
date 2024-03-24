using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{

   
   [SerializeField] List<string> messages; 

   float timer =0f;
    float popUpTimeout =2f;
    bool showText =false;

   TextMeshPro text;

    public Vector3 offset = new Vector3 (0f, 2f,0f );  

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (showText){
            timer+= Time.deltaTime;

            if (timer> popUpTimeout) {
                showText =false;
                popOut();
            }
        }

    }

    public void PopUpTimeout(int messageId =0 ){
        showText =true;
        timer =0f;
        popUp(messageId);
    }
    public void popUp(int messageId = 0)
    {
        

        Vector3  spawnPos  = this.transform.position +  offset; 


            GameObject popup = Instantiate (GameManager.i.PopUpPrefab,spawnPos, Quaternion.identity);

            text=  popup.GetComponent<TextMeshPro>();
            if (messages.Count != 0)  text.SetText(messages[messageId]);



    }

   public void popOut()
    {
        if (text!= null) Destroy(text.gameObject);
    }
}
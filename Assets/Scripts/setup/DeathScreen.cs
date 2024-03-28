using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DeathScreen : MonoBehaviour
{
   

   public float timerLoad ; 
   public float  Text1Start ;
   public float Text1End; 

   public float  Text2Start ;
   public float Text2End; 

   public float exit;

   public TMP_Text text1;

   public TMP_Text text2;
   
   public float timer = 0;

    void Start()
    {
        Text1Start += timerLoad ;
        Text1End += Text1Start ;
        Text2Start += Text1End ;
        Text2End += Text2Start ;
        exit += Text2End ;

        render = GetComponent<Image>();
        material = render.material;

        render.enabled = false;

        text2.SetText(text2.text + GameManager.Instance.attempt);

        //render.CrossFadeAlpha(0, 0, false);
    }

    Image render;

    Material material;




    // Update is called once per frame
    void Update()
    {
        if (timer >= 0){
            timer += Time.deltaTime;
            
            switch (timer) {

                case float t when t <timerLoad :


                    break; 
                case float t when t >= timerLoad && t < Text1Start :
                    render.enabled = true;
                break;

                case float t when t >= Text1Start && t < Text1End :
                    text1.gameObject.SetActive(true);
                    break;

                case float t when t >= Text1End && t < Text2Start :
                    text1.gameObject.SetActive(false);
                    break;

                case float t when t >= Text2Start && t < Text2End :
                    text2.gameObject.SetActive(true);
                    break;
                case float t when t >= Text2End && t < exit:
                    text2.gameObject.SetActive(false);
                    break; 
                case float t when t > exit:

                    InventoryManager.Instance.FailedRun();
                    GameManager.Instance.BackToMenu();
                    timer = -1;
                break;
            }





        }
    }

}

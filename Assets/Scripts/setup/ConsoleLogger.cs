using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class ConsoleLogger : MonoBehaviour
{

    public float timeOnScreen;
    public float timer = -1f;

    public TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox = this.gameObject.GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer != -1f )
            {
            timer += Time.deltaTime;

            if ( timer > timeOnScreen )
            {
                gameObject.GetComponent<TMP_Text>().SetText("");
                timer = -1f;
            }
        }
    }

    public void Show(string text )
    {
        gameObject.GetComponent<TMP_Text>().SetText( text);
        timer = 0f;
    }

}

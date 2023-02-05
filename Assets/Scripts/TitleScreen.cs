using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang

public class TitleScreen : MonoBehaviour
{

    [SerializeField] GameObject StartPrompt;
    [SerializeField] float blinkTimer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        blinkOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void blinkOff()
    {
        StartPrompt.SetActive(false);
        Invoke("blinkOn", blinkTimer);
    }

    void blinkOn()
    {
        StartPrompt.SetActive(true);
        Invoke("blinkOff", blinkTimer);
    }
}

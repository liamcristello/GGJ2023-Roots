using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang

public class BackgroundMusic : MonoBehaviour
{

    [SerializeField] AudioSource IntroBG;
    [SerializeField] AudioSource LoopBG;

    // Start is called before the first frame update
    void Start()
    {
        IntroBG.Play();
        LoopBG.PlayDelayed(IntroBG.clip.length);
    }
}

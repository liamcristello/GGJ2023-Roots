using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingMusic : MonoBehaviour
{
    public AudioClip battleStart;
    public AudioClip mainBattle;
    public AudioClip endGame;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = battleStart;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = mainBattle;
        GetComponent<AudioSource>().Play();
    }
}

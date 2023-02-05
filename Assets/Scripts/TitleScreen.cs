using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//author: Quang

public class TitleScreen : MonoBehaviour
{

    [SerializeField] GameObject StartPrompt;
    [SerializeField] float blinkTimer = 1.0f;
    [SerializeField] AudioSource titleMusic;

    // Start is called before the first frame update
    void Start()
    {
        blinkOn();
        playMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            loadGame();
        }
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

    void loadGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Loading gameplay");
    }

    void playMusic()
    {
        titleMusic.Play();
        Invoke("playMusic", titleMusic.clip.length + 2);
    }
}

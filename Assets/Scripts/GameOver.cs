using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCombat playerCombat;

    public AudioClip gameOverClip;
    public AudioClip gameOverSong;
    public AudioSource source;

    public GameObject bgMusic;

    public void EndGame()
    {
        playerController.enabled = false;
        playerCombat.enabled = false;
        source.clip = gameOverClip;
        bgMusic.GetComponent<BackgroundMusic>().StopAllSounds();
        source.PlayOneShot(gameOverClip);

        StartCoroutine(ShowGameOver());
    }

    public IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(1.0f);
        source.clip = gameOverSong;
        source.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }
}

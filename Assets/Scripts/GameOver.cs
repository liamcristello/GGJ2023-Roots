using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCombat playerCombat;

    public void EndGame()
    {
        playerController.enabled = false;
        playerCombat.enabled = false;
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

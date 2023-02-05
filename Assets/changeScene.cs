using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("GameOver");
        }
        if (Input.GetKey(KeyCode.A))
        {
            SceneManager.LoadScene("TonyScene");
        }
    }
    
}

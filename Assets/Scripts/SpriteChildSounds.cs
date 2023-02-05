using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChildSounds : MonoBehaviour
{
    public GameObject parent;

    public void PlayBiteSound()
    {
        parent.GetComponent<BulbBehavior>().PlayBiteSound();
    }

    public void PlayExplodeSound()
    {
        parent.GetComponent<BulbBehavior>().PlayExplodeSound();
    }
}

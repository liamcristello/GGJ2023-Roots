using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang
public class BombInteract : MonoBehaviour
{
    [SerializeField] float throwDelay = 0.1f;
    [SerializeField] AudioSource pickup;
    [SerializeField] AudioSource throwing;

    public bool throwReady = false;

    SpriteRenderer carriedBombSprite;

    // Start is called before the first frame update
    void Start()
    {
        carriedBombSprite = GetComponent<SpriteRenderer>();
        carriedBombSprite.enabled = false;
    }

    public void PickupBomb()
    {
        if (!pickup.isPlaying)
        {
            pickup.Play();
        }
        carriedBombSprite.enabled = true;
        Invoke("ReadyThrow", throwDelay);
        Debug.Log("Picked up a bomb!");
    }

    void ReadyThrow()
    {
        throwReady = true;
    }

    public void ThrowBomb()
    {
        if (!throwing.isPlaying)
        {
            throwing.Play();
        }
        carriedBombSprite.enabled = false;
        throwReady = false;
        Debug.Log("Threw a bomb!");
    }
}

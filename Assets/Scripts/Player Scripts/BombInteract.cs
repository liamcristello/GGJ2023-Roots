using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang
public class BombInteract : MonoBehaviour
{
    [SerializeField] float throwDelay = 0.1f;

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
        carriedBombSprite.enabled = false;
        throwReady = false;
        Debug.Log("Threw a bomb!");
    }
}

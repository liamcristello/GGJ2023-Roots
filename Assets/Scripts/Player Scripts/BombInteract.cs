using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang
public class BombInteract : MonoBehaviour
{
    SpriteRenderer carriedBombSprite;

    // Start is called before the first frame update
    void Start()
    {
        carriedBombSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        carriedBombSprite.enabled = false;
    }

    public void PickupBomb()
    {
        carriedBombSprite.enabled = true;
    }

    void ThrowBomb()
    {
        carriedBombSprite.enabled = false;
    }
}

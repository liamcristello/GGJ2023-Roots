using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang
public class BombFlowers : MonoBehaviour
{
    SpriteRenderer flowerSprite;
    CircleCollider2D flowerCollider;
    Animator flowerAnimator;

    float growthTimer = 0.0f;
    bool bombReady = false;

    [SerializeField] float readyTimer = 10.0f;
    [SerializeField] float pickupRange = 10.0f;
    [SerializeField] GameObject player; //This should be the player gameobject with the actual sprite attached to it
    [SerializeField] GameObject playerBomb;
    [SerializeField] GameObject bombFlower;
    BombInteract playerBombInteract;

    // Start is called before the first frame update
    void Start()
    {
        flowerSprite = GetComponent<SpriteRenderer>();
        playerBombInteract = playerBomb.GetComponent<BombInteract>();
        flowerAnimator = bombFlower.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(growthTimer < readyTimer)
        {
            growthTimer += Time.deltaTime;
        }
        else if(!bombReady)
        {
            SproutBomb();
        }    
    }

    void SproutBomb()
    {
        bombReady = true;
        //flowerSprite.color = Color.green;
    }

    void Debomb()
    {
        bombReady = false;
        flowerAnimator.SetTrigger("Replant");
        growthTimer = 0.0f;
        //flowerSprite.color = Color.red;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (bombReady && inDist() && !playerBombInteract.throwReady)
            {
                Debomb();
                playerBombInteract.PickupBomb();
            }
        }
    }

    bool inDist()
    {
        Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if(Vector3.Distance(player.transform.position, transform.position) < pickupRange)
        {
            return true;
        }
        return false;
    }
}

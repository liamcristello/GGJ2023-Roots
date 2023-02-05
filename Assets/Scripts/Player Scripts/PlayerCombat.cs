using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author:Quang
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float atkTimer = 0.0f;  //Tracks how long its been since atk went off
    [SerializeField] float atkLength = 0.25f; //How long atk takes to finish
    [SerializeField] float atkRange = 10.0f ;  //Range of atk

    [SerializeField] GameObject swordSprite;
    [SerializeField] GameObject swordHB;
    [SerializeField] GameObject playerBomb;

    BombInteract playerBombInteract;
    BoxCollider2D swordCollider;
    SpriteRenderer swordSwing;
    Animator swordAnim;

    //public bool swingFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBombInteract = playerBomb.GetComponent<BombInteract>();
        swordSwing = swordSprite.GetComponent<SpriteRenderer>();
        swordAnim = swordSprite.GetComponent<Animator>();
        swordCollider = swordHB.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(atkTimer <= 0 && !playerBombInteract.throwReady)
        {
            PlayerAttak();
            //swingFlag = false;
            //swordAnim.
        }
        atkTimer -= Time.deltaTime;

        swordSwing.enabled = true;
        if (playerBombInteract.throwReady)
        {
            swordSwing.enabled = false;
        }
    }

    void SwingAnimation()
    {
        //WORK ON THIS
        //swordSwing.enabled = true;
        if (atkTimer <= 0)
        {
            swordCollider.enabled = false;
            //swordSwing.enabled = false;
        }
        //swordAnim.Play("Base Layer.swing", 0, 0.0f);
        //swordAnim.SetTrigger("Swing");
    }

    void PlayerAttak()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack!");
            atkTimer = atkLength;
            SwingAnimation();
            swordAnim.SetTrigger("Swing");

            swordCollider.enabled = true;
            Invoke("disableHB", atkLength);
        }
    }

    void disableHB()
    {
        swordCollider.enabled = false;
    }
}

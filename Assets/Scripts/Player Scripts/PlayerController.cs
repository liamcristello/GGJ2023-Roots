using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    [SerializeField] float moveLimiter = 0.7f;
    [SerializeField] float runSpeed = 300.0f;

    Animator playerAnimator;
    SpriteRenderer playerSprite;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if(horizontal != 0 || vertical != 0)
        {
            playerAnimator.SetFloat("XInput", horizontal);
            playerAnimator.SetFloat("YInput", vertical);
            playerAnimator.SetTrigger("Walking");
        }
        else
        {
            playerAnimator.SetTrigger("Idle");
        }

        CheckOrientation();
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed * Time.deltaTime, vertical * runSpeed * Time.deltaTime);
    }

    void CheckOrientation()
    {
        if(horizontal != 0)
        {
            if (horizontal < 0)
            {
                playerSprite.flipX = true;
            }
            else
            {
                playerSprite.flipX = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author:Quang
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] float atkTimer = 0.0f;  //Tracks how long its been since atk went off
    [SerializeField] float atkLength = .5f; //How long atk takes to finish
    [SerializeField] float atkRange = 10.0f ;  //Range of atk

    [SerializeField] GameObject swordHB;
    SpriteRenderer swordSwing;

    // Start is called before the first frame update
    void Start()
    {
        swordSwing = swordHB.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(atkTimer <= 0)
        {
            PlayerAttak();
        }
        SwingAnimation();
        atkTimer -= Time.deltaTime;
    }

    void SwingAnimation()
    {
        swordSwing.enabled = true;
        if (atkTimer <= 0)
        {
            swordSwing.enabled = false;
        }
    }

    void PlayerAttak()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack!");
            atkTimer = atkLength;
            SwingAnimation();
        }
    }
}

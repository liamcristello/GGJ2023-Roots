using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    Transform swordTransform;
    BoxCollider2D swordHB;

    private void Start()
    {
        swordHB = GetComponent<BoxCollider2D>();
        swordHB.enabled = false;
        swordTransform = GetComponent<Transform>();
    }

    private void Update()
    {
            
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            swordHB.enabled = false;
            Debug.Log("SLAIN AN ENEMY");
        }
    }
}

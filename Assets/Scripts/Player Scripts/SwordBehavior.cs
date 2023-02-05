using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Quang
//ATTACH MAIN CAMERA TO THE PLAYERBODY GAMEOBJECT FOR SWORD ROTATION TO WORK PROPERLY

public class SwordBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject swordWhole;
    
    Transform swordTransform;
    BoxCollider2D swordHB;
    Vector2 positionOnScreen;//tracks player position
    Vector2 mouseOnScreen;//tracks mouseposition

    float swordAngle;

    private void Start()
    {
        swordHB = GetComponent<BoxCollider2D>();
        swordHB.enabled = false;
        swordTransform = swordWhole.GetComponent<Transform>();
    }

    private void Update()
    {
        positionOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        rotateSword();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        swordHB.enabled = false;
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("SLAIN AN ENEMY");
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void rotateSword()
    {
        swordAngle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //Debug.Log(swordAngle);

        //swordAngle = Vector2.SignedAngle(Input.mousePosition - player.transform.position, Vector2.right);
        swordTransform.eulerAngles = new Vector3(0, 0, (swordAngle - 180));
        //swordTransform.LookAt(Input.mousePosition);
    }
}

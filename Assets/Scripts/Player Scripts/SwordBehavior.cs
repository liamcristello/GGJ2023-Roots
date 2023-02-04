using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//author: Quang
public class SwordBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject swordWhole;

    Transform swordTransform;
    BoxCollider2D swordHB;

    float swordAngle;

    private void Start()
    {
        swordHB = GetComponent<BoxCollider2D>();
        swordHB.enabled = false;
        swordTransform = swordWhole.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(player.transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        swordAngle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //Debug.Log(swordAngle);

        //swordAngle = Vector2.SignedAngle(Input.mousePosition - player.transform.position, Vector2.right);
        swordTransform.eulerAngles = new Vector3(0, 0, swordAngle + 180);
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
}

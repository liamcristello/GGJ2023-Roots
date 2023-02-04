using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEndBehavior : MonoBehaviour
{
    public bool atEnd = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrowTarget"))
        {
            atEnd = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrowTarget"))
        {
            atEnd = false;
        }
    }
}

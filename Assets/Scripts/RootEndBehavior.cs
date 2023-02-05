using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEndBehavior : MonoBehaviour
{
    public bool atEnd = false;

    public BulbBehavior bulbBehavior;

    public float feedValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrowTarget"))
        {
            Debug.Log("Reached end at " + gameObject.name);
            StartCoroutine(StopGrowing());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Debug.Log("Sword appears at " + gameObject.name);
            bulbBehavior.TakeDamage();
        }
    }

    IEnumerator StopGrowing()
    {
        atEnd = true;
        yield return new WaitForSecondsRealtime(bulbBehavior.timeToGrowSegment);
        StartCoroutine(FeedPlant());
    }

    IEnumerator FeedPlant()
    {
        bulbBehavior.LerpPlantSlider(feedValue);
        yield return null;
        if (atEnd)
        {
            StartCoroutine(FeedPlant());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrowTarget"))
        {
            atEnd = false;
            Debug.Log(gameObject.name + " can start growing again");
            StartCoroutine(bulbBehavior.GrowRoots());
        }
    }
}

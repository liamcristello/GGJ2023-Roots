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
            StartCoroutine(StopGrowing());
        }
    }

    IEnumerator StopGrowing()
    {
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
            StartCoroutine(bulbBehavior.GrowRoots());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbBehavior : MonoBehaviour
{
    public float timeToGrowSegment;
    private float growthTimer;

    public GameObject growTarget;
    public GameObject rootExtender;
    private float rootGrowthStep;
    public float rootGrowthSpeed;
    public GameObject rootOrigin;
    public GameObject rootSegmentPrefab;
    public List<GameObject> rootSegmentsList;

    public bool atEnd;

    // Start is called before the first frame update
    void Start()
    {
        rootGrowthStep = rootSegmentPrefab.GetComponent<SpriteRenderer>().localBounds.extents.y * 2;
        Debug.Log(gameObject.name + ": " + rootGrowthStep);

        atEnd = false;

        growthTimer = 0.0f;
        if (timeToGrowSegment <= 0.0f)
        {
            // Hard-coded default in event of float not being set
            timeToGrowSegment = 4.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        growthTimer += Time.deltaTime;

        if (growthTimer > timeToGrowSegment && !atEnd)
        {
            Grow();
            growthTimer = 0.0f;
        }
    }

    void Grow()
    {
        foreach (var rootSegment in rootSegmentsList)
        {
            rootSegment.transform.position = Vector2.MoveTowards(rootSegment.transform.position, growTarget.transform.position, rootGrowthStep);
        }
        AddRootSegment();
    }

    void AddRootSegment()
    {
        GameObject newRootSeg = Instantiate(rootSegmentPrefab, rootOrigin.transform);

        rootSegmentsList.Add(newRootSeg);
    }

    void RemoveRootSegment()
    {
        rootSegmentsList.RemoveAt(rootSegmentsList.Count - 1);
    }
}

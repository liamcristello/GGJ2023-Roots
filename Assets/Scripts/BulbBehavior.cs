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
    public GameObject rootSegment;
    public GameObject rootEnd;

    // Start is called before the first frame update
    void Start()
    {
        rootGrowthStep = rootExtender.GetComponent<SpriteRenderer>().bounds.size.y;

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

        if (growthTimer > timeToGrowSegment)
        {
            Grow();
            growthTimer = 0.0f;
        }
    }

    void Grow()
    {
        rootExtender.transform.position = Vector2.MoveTowards(rootExtender.transform.position, growTarget.transform.position, rootGrowthStep);
    }
}

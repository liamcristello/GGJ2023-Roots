using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulbBehavior : MonoBehaviour
{
    public float timeToGrowSegment;
    private float growthTimer;

    public GameObject growTarget;
    public float rootGrowthSpeed;
    public GameObject rootOrigin;
    public GameObject rootEnd;
    public GameObject rootSegmentPrefab;
    public List<GameObject> rootSegmentsList;
    public float rootGrowthStepX;
    public float rootGrowthStepY;
    public Animator rootEndAnim;

    public bool atEnd;

    // Start is called before the first frame update
    void Start()
    {
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

        if (growthTimer > timeToGrowSegment && !rootEnd.GetComponent<RootEndBehavior>().atEnd)
        {
            Grow();
            growthTimer = 0.0f;
        }

        if (Input.GetMouseButtonDown(0) && rootSegmentsList.Count > 1)
        {
            Retract();
        }
    }

    void Grow()
    {
        foreach (var rootSegment in rootSegmentsList)
        {
            float newX = rootSegment.transform.position.x + rootGrowthStepX;
            float newY = rootSegment.transform.position.y + rootGrowthStepY;
            rootSegment.transform.position = new Vector3(newX, newY, rootSegment.transform.position.z);
        }
        AddRootSegment();
    }

    void Retract()
    {
        RemoveInnermostRootSegment();
        foreach (var rootSegment in rootSegmentsList)
        {
            float newX = rootSegment.transform.position.x + (-1 * rootGrowthStepX);
            float newY = rootSegment.transform.position.y + (-1 * rootGrowthStepY);
            rootSegment.transform.position = new Vector3(newX, newY, rootSegment.transform.position.z);
        }
        //growthTimer = 0.0f;
    }

    void AddRootSegment()
    {
        GameObject newRootSeg = Instantiate(rootSegmentPrefab, rootOrigin.transform);

        rootSegmentsList.Add(newRootSeg);

        rootEndAnim.Play("RootEndIdle");
    }

    void RemoveInnermostRootSegment()
    {
        Destroy(rootSegmentsList[rootSegmentsList.Count - 1]);
        rootSegmentsList.RemoveAt(rootSegmentsList.Count - 1);
    }
}

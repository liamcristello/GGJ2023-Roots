using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulbBehavior : MonoBehaviour
{
    public float timeToGrowSegment;
    public GameObject growTarget;

    public GameObject rootOrigin;
    public GameObject rootEnd;

    public GameObject rootSegmentPrefab;
    public GameObject damagedRootSegmentPrefab;

    public List<GameObject> rootSegmentsList;
    public List<GameObject> damagedRootSegmentsList;

    public float rootGrowthStepX;
    public float rootGrowthStepY;

    public Animator rootEndAnim;
    public Animator damagedRootEndAnim;

    private bool isBeingDamaged;
    public float damageFlashDuration;

    public Slider plantSlider;

    // Start is called before the first frame update
    void Start()
    {
        SetGrowSpeed(timeToGrowSegment);

        StartCoroutine(GrowRoots());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rootSegmentsList.Count > 1)
        {
            TakeDamage();
        }
    }

    void SetGrowSpeed(float timeToGrowSegment)
    {
        rootEndAnim.speed = 1 / timeToGrowSegment;
        damagedRootEndAnim.speed = 1 / timeToGrowSegment;
    }

    public IEnumerator GrowRoots()
    {
        yield return new WaitForSecondsRealtime(timeToGrowSegment);

        if (!rootEnd.GetComponent<RootEndBehavior>().atEnd)
        {
            LengthenRoot();
            StartCoroutine(GrowRoots());
        }
    }

    void LengthenRoot()
    {
        Debug.Log("Lengthening");
        foreach (var rootSegment in rootSegmentsList)
        {
            MoveSegment(rootSegment, false);
        }
        foreach (var damagedRootSegment in damagedRootSegmentsList)
        {
            MoveSegment(damagedRootSegment, false);
        }
        AddRootSegment();
    }

    void MoveSegment(GameObject rootSegment, bool isRetracting)
    {
        float stepX = rootGrowthStepX;
        float stepY = rootGrowthStepY;

        if (isRetracting)
        {
            stepX *= -1;
            stepY *= -1;
        }

        float newX = rootSegment.transform.position.x + stepX;
        float newY = rootSegment.transform.position.y + stepY;
        rootSegment.transform.position = new Vector3(newX, newY, rootSegment.transform.position.z);
    }

    void TakeDamage()
    {
        if (!isBeingDamaged)
        {
            RetractRoot();

            foreach (GameObject damagedRootSegment in damagedRootSegmentsList)
            {
                StartCoroutine(DamageVisual(damagedRootSegment, damageFlashDuration));
            }
        }
    }

    /// <summary>
    /// Play the flashing white animation
    /// </summary>
    /// <param name="damagedRootSegment">The damaged root segment object</param>
    /// <param name="duration">How long the WHOLE ANIMATION should take</param>
    /// <returns></returns>
    IEnumerator DamageVisual(GameObject damagedRootSegment, float duration)
    {
        isBeingDamaged = true;
        SpriteRenderer rend = damagedRootSegment.GetComponent<SpriteRenderer>();
        float alpha = 0.0f;

        float timeElapsed = 0;
        duration /= 2;
        while (timeElapsed < duration)
        {
            alpha = Mathf.Lerp(alpha, 1.0f, timeElapsed / duration);
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        alpha = 1.0f;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);

        timeElapsed = 0;
        while (timeElapsed < duration)
        {
            alpha = Mathf.Lerp(alpha, 0.0f, timeElapsed / duration);
            rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        alpha = 0.0f;
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);
        isBeingDamaged = false;
    }

    void RetractRoot()
    {
        RemoveInnermostRootSegment();
        foreach (var rootSegment in rootSegmentsList)
        {
            MoveSegment(rootSegment, true);
        }
        foreach (var damagedRootSegment in damagedRootSegmentsList)
        {
            MoveSegment(damagedRootSegment, true);
        }
    }

    void AddRootSegment()
    {
        GameObject newRootSeg = Instantiate(rootSegmentPrefab, rootOrigin.transform);
        rootSegmentsList.Add(newRootSeg);
        GameObject newDamagedRootSeg = Instantiate(damagedRootSegmentPrefab, rootOrigin.transform);
        damagedRootSegmentsList.Add(newDamagedRootSeg);

        rootEndAnim.Play("RootEndIdle");
        damagedRootEndAnim.Play("RootEndIdle");
    }

    void RemoveInnermostRootSegment()
    {
        Destroy(rootSegmentsList[rootSegmentsList.Count - 1]);
        rootSegmentsList.RemoveAt(rootSegmentsList.Count - 1);

        Destroy(damagedRootSegmentsList[damagedRootSegmentsList.Count - 1]);
        damagedRootSegmentsList.RemoveAt(damagedRootSegmentsList.Count - 1);
    }

    public void LerpPlantSlider(float feedVal)
    {
        if (plantSlider.value < 1.0f)
        {
            plantSlider.value = Mathf.Lerp(plantSlider.value, plantSlider.value + feedVal, Time.deltaTime);
            Debug.Log("PLANT IS " + (plantSlider.value * 100) + "% FULL");
        }
    }
}

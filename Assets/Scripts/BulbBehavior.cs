using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulbBehavior : MonoBehaviour
{
    public float timeToGrowSegment;
    public GameObject growTarget;

    public GameObject player;
    public GameObject playerBomb;
    public float bombRange;

    public GameObject rootOrigin;
    public GameObject rootEnd;
    //public GameObject secondToLastRoot;

    public GameObject rootSegmentPrefab;
    public GameObject damagedRootSegmentPrefab;
    public GameObject rootBeforeEndPrefab;
    public GameObject damagedRootBeforeEndPrefab;

    public List<GameObject> rootSegmentsList;
    public List<GameObject> damagedRootSegmentsList;

    public float rootGrowthStepX;
    public float rootGrowthStepY;

    public Animator rootEndAnim;
    public Animator damagedRootEndAnim;
    private Animator rootBeforeEndAnim;
    private Animator damagedRootBeforeEndAnim;

    private bool isBeingDamaged;
    public float damageFlashDuration;
    public float stunDuration;
    private bool stunned;

    public Slider plantSlider;

    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);

        SetGrowSpeed(timeToGrowSegment);

        stunned = false;

        StartCoroutine(GrowRoots());

        player = GameObject.FindGameObjectWithTag("Player");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Sword") && !stunned)
    //    {
    //        StartCoroutine(Stun());
    //    }
    //}

    void SetGrowSpeed(float timeToGrowSegment)
    {
        rootEndAnim.speed = 1 / timeToGrowSegment;
        damagedRootEndAnim.speed = 1 / timeToGrowSegment;

        if (rootBeforeEndAnim)
        {
            rootBeforeEndAnim.speed = 1 / timeToGrowSegment;
        }
        if (damagedRootBeforeEndAnim)
        {
            damagedRootBeforeEndAnim.speed = 1 / timeToGrowSegment;
        }
    }

    public IEnumerator GrowRoots()
    {
        yield return new WaitForSecondsRealtime(timeToGrowSegment);

        if (!stunned && !rootEnd.GetComponent<RootEndBehavior>().atEnd)
        {
            LengthenRoot();
            StartCoroutine(GrowRoots());
        }
    }

    void LengthenRoot()
    {
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

    public void TakeDamage()
    {
        if (!isBeingDamaged && rootSegmentsList.Count > 1)
        {
            RetractRoot();

            foreach (GameObject damagedRootSegment in damagedRootSegmentsList)
            {
                Debug.Log("Damaging " + damagedRootSegment.name);
                StartCoroutine(DamageVisual(damagedRootSegment, damageFlashDuration));
            }
        }
    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < bombRange)
        {
            Debug.Log("Bomb!");
        }
    }

        public IEnumerator Stun()
    {
        stunned = true;
        TakeDamage();
        SetGrowSpeed(0.0f);
        StopCoroutine(GrowRoots());

        yield return new WaitForSecondsRealtime(stunDuration);
        SetGrowSpeed(timeToGrowSegment);
        if (rootEndAnim)
        {
            rootEndAnim.Play("RootEndIdle");
            damagedRootEndAnim.Play("RootEndIdle");
        }
        if (rootBeforeEndAnim)
        {
            rootBeforeEndAnim.Play("RootEndIdle");
            damagedRootBeforeEndAnim.Play("RootEndIdle");
        }
        stunned = false;
        StartCoroutine(GrowRoots());
    }

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

        // Same thing in reverse
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
        if (rootSegmentsList.Count > 1)
        {
            GameObject newRootSeg = Instantiate(rootSegmentPrefab, rootOrigin.transform);
            rootSegmentsList.Add(newRootSeg);
            GameObject newDamagedRootSeg = Instantiate(damagedRootSegmentPrefab, rootOrigin.transform);
            damagedRootSegmentsList.Add(newDamagedRootSeg);
        }
        else
        {
            GameObject newRootBeforeEndSeg = Instantiate(rootBeforeEndPrefab, rootOrigin.transform);
            rootSegmentsList.Add(newRootBeforeEndSeg);
            rootBeforeEndAnim = newRootBeforeEndSeg.GetComponent<Animator>();
            GameObject newDamagedRootBeforeEndSeg = Instantiate(damagedRootBeforeEndPrefab, rootOrigin.transform);
            damagedRootSegmentsList.Add(newDamagedRootBeforeEndSeg);
            damagedRootBeforeEndAnim = newDamagedRootBeforeEndSeg.GetComponent<Animator>();
            SetGrowSpeed(timeToGrowSegment);
        }

        rootEndAnim.Play("RootEndIdle");
        damagedRootEndAnim.Play("RootEndIdle");
        rootBeforeEndAnim.Play("RootEndIdle");
        damagedRootBeforeEndAnim.Play("RootEndIdle");
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

            if (plantSlider.value >= 1.0f)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOver.SetActive(true);
    }
}

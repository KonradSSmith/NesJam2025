using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlacementChecker : MonoBehaviour
{
    [SerializeField] private AgentDriving agent;
    [SerializeField] public bool player = false;
    [SerializeField] private GameObject firstCheckpoint;
    public int lapsCompleted = 0;
    public float health = 100;
    [SerializeField] public Jousting jousting;
    [SerializeField] public Image spriteRendererMultiplier;
    public float joustingRangeAI;
    public float multiplier;
    [SerializeField] GameObject lapObject;
    public bool willJoust = false;

    public GameObject nextCheckpoint;
    public float placementDistance;
    public float playerPlacementDistance;
    public int intPlacement;
    public float playerPlacement;

    private void Start()
    {
        setCheckpoint(firstCheckpoint);
    }

    public void setCheckpoint(GameObject newCheckpoint)
    {
        if (newCheckpoint.GetComponent<CheckpointScript>().ID == firstCheckpoint.GetComponent<CheckpointScript>().ID - 1)
        {
            lapsCompleted += 1;
            if (player && lapsCompleted > 1)
            {
                StartCoroutine(LapCompleted());
            }
        }
        nextCheckpoint = newCheckpoint;

        if (agent != null)
        {
            agent.nextCheckpoint = newCheckpoint;
        }
    }

    private void Update()
    {
        placementDistance = Vector3.Distance(nextCheckpoint.transform.position, transform.position) + nextCheckpoint.GetComponent<CheckpointScript>().ID * 50 - (lapsCompleted * 10000);
        if ((placementDistance > playerPlacementDistance) && PlacementManager.instance.racing && intPlacement > playerPlacement)
        {
            if (willJoust)
            {
                StartCoroutine(QTEUI.instance.indicator(gameObject.transform));
            }
        }
        if (player && health <= 0)
        {
            //you die
        }
    }

    IEnumerator LapCompleted()
    {
        StartCoroutine(AudioManager.instance.LapSound());
        health += 50;
        if (health > 100)
        {
            health = 100;
        }
        lapObject.SetActive(true);
        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < 5; i++)
        {
            lapObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            lapObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        lapObject.SetActive(false);
    }
}

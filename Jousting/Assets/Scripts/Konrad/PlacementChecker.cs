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
    public float multiplier;

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
    }

}

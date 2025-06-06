using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    [SerializeField] private AgentDriving agent;
    [SerializeField] public bool player = false;
    [SerializeField] private GameObject firstCheckpoint;
    public bool lapsCompleted;

    public GameObject nextCheckpoint;
    public float placementDistance;
    public float playerPlacementDistance;
    public float intPlacement;
    public float playerPlacement;

    private void Start()
    {
        setCheckpoint(firstCheckpoint);
    }
    public void setCheckpoint(GameObject newCheckpoint)
    {
        nextCheckpoint = newCheckpoint;

        if (agent != null)
        {
            agent.nextCheckpoint = newCheckpoint;
        }
    }

    private void Update()
    {
        placementDistance = Vector3.Distance(nextCheckpoint.transform.position, transform.position) + nextCheckpoint.GetComponent<CheckpointScript>().ID * 50;
    }

}

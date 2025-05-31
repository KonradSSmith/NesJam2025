using UnityEngine;

public class PlacementChecker : MonoBehaviour
{
    [SerializeField] private AgentDriving agent;
    [SerializeField] private GameObject firstCheckpoint;

    private void Start()
    {
        setCheckpoint(firstCheckpoint);
    }
    public void setCheckpoint(GameObject newCheckpoint)
    {
        if (agent != null)
        {
            agent.nextCheckpoint = newCheckpoint;
            agent.placementCheckpointModifier = newCheckpoint.GetComponent<CheckpointScript>().ID * 50;
        }
        
    }

}

using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    [SerializeField] public GameObject nextCheckpoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.GetComponent<AgentDriving>() != null)
        {
            other.gameObject.GetComponent<AgentDriving>().setCheckpoint(nextCheckpoint);
        }
    }
}

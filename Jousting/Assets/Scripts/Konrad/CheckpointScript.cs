using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] public float ID;
    [SerializeField] public GameObject nextCheckpoint;
    [SerializeField] public bool finalCheckpoint = false;


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
       if (other.gameObject.GetComponent<PlacementChecker>() != null)
        {
            if (!finalCheckpoint)
            {
                other.gameObject.GetComponent<PlacementChecker>().setCheckpoint(nextCheckpoint);
            }
        }
    }
}

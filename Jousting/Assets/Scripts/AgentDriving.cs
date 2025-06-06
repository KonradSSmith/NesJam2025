using System.Collections;
using UnityEngine;

public class AgentDriving : MonoBehaviour
{
    public GameObject nextCheckpoint;
    public float placementCheckpointModifier;
    [SerializeField] Rigidbody rb;
    public bool racing = false;
    public float placementDistance;

    Vector3 moveDirection = Vector3.zero;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 100f;

    private void Start()
    {
        startRace();
    }

    // Update is called once per frame
    void Update()
    {
        placementDistance = Vector3.Distance(nextCheckpoint.transform.position, transform.position) + placementCheckpointModifier;
    }

    private void FixedUpdate()
    {
        if (racing)
        {
            moveDirection = (nextCheckpoint.transform.position - transform.position).normalized;
            rb.AddForce(moveDirection * acceleration);
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, moveSpeed);
        }
    }
    
    IEnumerator setSpeed()
    {
        while (racing)
        {
            moveSpeed = Random.Range(20, 55);
            acceleration = moveSpeed;
            yield return new WaitForSeconds(Random.Range(1, 3));
        }

        yield return null;
    }

    public void startRace()
    {
        racing = true;
        StartCoroutine(setSpeed());
    }
}

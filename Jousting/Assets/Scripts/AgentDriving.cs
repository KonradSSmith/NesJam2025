using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class AgentDriving : MonoBehaviour
{
    [SerializeField] private GameObject nextCheckpoint;
    [SerializeField] Rigidbody rb;
    bool racing = false;

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
            moveSpeed = Random.Range(3, 6);
            yield return new WaitForSeconds(Random.Range(1, 4));
        }

        yield return null;
    }

    public void startRace()
    {
        racing = true;
        StartCoroutine(setSpeed());
    }

    public void setCheckpoint(GameObject newCheckpoint)
    {
        nextCheckpoint = newCheckpoint;
    }
}

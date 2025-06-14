using System.Collections;
using UnityEngine;

public class AgentDriving : MonoBehaviour
{
    public GameObject nextCheckpoint;
    [SerializeField] private PlacementChecker checker;

    [SerializeField] Rigidbody rb;
    public bool racing = false;
    public bool slowed = false;

    Vector3 moveDirection = Vector3.zero;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 100f;

    private void Start()
    {
        //startRace();
    }

    private void Update()
    {
        if (slowed)
        {
            moveSpeed = 0;
            acceleration = moveSpeed;
        }
        if (!racing && PlacementManager.instance.racing)
        {
            startRace();
        }
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
            if (checker.placementDistance > checker.playerPlacementDistance + 65)
            {
                moveSpeed = Random.Range(100, 130);
                acceleration = moveSpeed;
            }
            else if (checker.placementDistance < checker.playerPlacementDistance - 45)
            {
                moveSpeed = Random.Range(20, 45);
                acceleration = moveSpeed;
            }
            yield return new WaitForSeconds(Random.Range(1, 3));
        }

        yield return null;
    }

    public void startRace()
    {
        racing = true;
        StartCoroutine(setSpeed());
    }

    public void SlowDown()
    {
        slowed = true;
        StartCoroutine(SlowTime());
    }

    public void StopDriving()
    {
        racing = false;
        moveSpeed = 0;
        acceleration = moveSpeed;
    }

    IEnumerator SlowTime()
    {
        yield return new WaitForSeconds(2);
        slowed = false;
        moveSpeed = 90;
        acceleration = moveSpeed;

    }
}

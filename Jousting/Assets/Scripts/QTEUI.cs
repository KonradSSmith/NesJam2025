using System.Timers;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    public RectTransform pointA; // Reference to the starting point
    public RectTransform pointB; // Reference to the ending point
    public RectTransform safeZone; // Reference to the safe zone RectTransform
    public float moveSpeed = 100f; // Speed of the pointer movement

    private float direction = 1f; // 1 for moving towards B, -1 for moving towards A
    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        //transform.localPosition = new Vector3(-20, 0, 0) * Time.deltaTime * moveSpeed;
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        
    }

    void Update()
    {
        Debug.Log(pointerTransform.anchoredPosition);

        /*
                //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);
                if (pointerTransform.anchoredPosition.x <= -100)
                {
                    //go to pointB
                    Debug.Log("to pointB");
                    pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x + (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

                }
                else if(pointerTransform.anchoredPosition.x >= 98 )
                {
                    //go to point A
                    Debug.Log("to pointA");
                    //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

                }
                pointerTransform.anchoredPosition = new Vector2(Mathf.Clamp(pointerTransform.anchoredPosition.x, -100, 100), pointerTransform.anchoredPosition.y);
        */
        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Change direction if the pointer reaches one of the points
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
            direction = 1f;
            //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
            direction = -1f;
           // pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (-100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

        }

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }

    void CheckSuccess()
    {
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }
}
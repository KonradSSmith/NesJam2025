using System.Timers;
using Unity.VisualScripting;
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
    bool stopped = false;

    private InputSystem_Actions actions;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = new Vector3(pointB.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        //transform.localPosition = new Vector3(-20, 0, 0) * Time.deltaTime * moveSpeed;
        //pointerTransform.anchoredPosition = Vector3.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);
        
    }

    void Update()
    {
        if (!stopped)
        {

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
            pointerTransform.anchoredPosition = Vector2.MoveTowards(pointerTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);

            // Change direction if the pointer reaches one of the points
            if (Vector3.Distance(pointerTransform.anchoredPosition, pointA.anchoredPosition) < 0.1f)
            {
                targetPosition = new Vector3(pointB.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
                direction = 1f;
                //pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

            }
            else if (Vector3.Distance(pointerTransform.anchoredPosition, pointB.anchoredPosition) < 0.1f)
            {
                targetPosition = new Vector3(pointA.anchoredPosition.x, pointerTransform.anchoredPosition.y, 0);
                direction = -1f;
                // pointerTransform.anchoredPosition = new Vector2(pointerTransform.anchoredPosition.x - (-100 * Time.deltaTime), pointerTransform.anchoredPosition.y);

            }

            // Check for input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stopped = true;
                CheckSuccess();
            }
        }
    }

    void CheckSuccess()
    {
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position))
        {
            Debug.Log("Success!");
        }
        else
        {
            Debug.Log("Fail!");
        }
    }
}
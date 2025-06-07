using System;
using UnityEngine;

public class QTEUI : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions actions;

    [SerializeField] Transform leftLimmit;
    [SerializeField] Transform rightLimmit;
    [SerializeField] RectTransform safezone;
    [SerializeField] float pointerSpeed;

    private float dir = 1f;
    private RectTransform pointerTransform;
    private Vector3 targetPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*private void OnEnable()
    {
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        actions.Player.Disable();
    }*/
    void Start()
    {
        actions = new InputSystem_Actions();
        pointerTransform = GetComponent<RectTransform>();
        targetPos = rightLimmit.position;
    }

    // Update is called once per frame
    void Update()
    {
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPos, pointerSpeed * Time.deltaTime);
        //change direction
        if (Vector3.Distance(pointerTransform.position, leftLimmit.position) < 0.1f)
        {
            targetPos = rightLimmit.position;
            dir = 1f;
        }
        else if (Vector3.Distance(pointerTransform.position, rightLimmit.position) < 0.1f)
        {
            targetPos = leftLimmit.position;
            dir = -1f;
        }

        // Check for input
        //if (actions.Player.AButton.WasPressed) //|| actions.Player.BButton.IsPressed)
        if(Input.GetKeyDown(KeyCode.X))
        {
            Poke();
        }
    }

    private void Poke()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(safezone, pointerTransform.position, null))
        {
            Debug.Log("Won duel");
        }
        else
        {
            Debug.Log("loss");
        }
    }

}


using UnityEngine;

public class MoveJoustStick : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions actions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private Vector2 moveInput;
    private float speed = 0.4f;
    private float maxLeft = -0.246f;
    private float maxRight = 0.247f;
    public Vector3 leftTarget = new Vector3(-0.246f, 0, 0);
    public Vector3 rightTarget = new Vector3(0.247f, 0, 0);
    private void Awake()
    {
        actions = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
    }

    private void OnDisable()
    {
        actions.Player.Disable();
    }
    private void Update() //works but is kind of visually gittery
    {

        //trying movetowards next for the UI



        if (actions.Player.AButton.inProgress && transform.position.x > maxLeft)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
        if (actions.Player.BButton.inProgress && transform.position.x < maxRight)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
      /*
       *  transform.Translate(-transform.right * speed * Time.deltaTime);
       transform.Translate(transform.right * speed * Time.deltaTime);
        */

/*
 *  transform.position = Vector3.MoveTowards(transform.position, leftTarget, speed * Time.deltaTime);
 *   transform.position = Vector3.MoveTowards(transform.position, rightTarget, speed * Time.deltaTime);
 *   */
}


}

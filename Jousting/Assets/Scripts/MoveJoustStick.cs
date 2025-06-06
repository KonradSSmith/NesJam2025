using UnityEngine;

public class MoveJoustStick : MonoBehaviour
{
    [SerializeField] private InputSystem_Actions actions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private Vector2 moveInput;
    private float speed = 0.4f;
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
    private void Update() //still gotta put in the limmits it will go off screen
    {
        if (actions.Player.AButton.inProgress)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);

        }
        if (actions.Player.BButton.inProgress)
        {
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
      

        
    }

   
}

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private InputSystem_Actions actions;
    [SerializeField] private float minCameraRotationAmount;
    [SerializeField] private float cameraRotationSpeed;
    private Rigidbody rb;
    private Vector2 moveInput;


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

    private void FixedUpdate()
    {
        moveInput = actions.Player.Move.ReadValue<Vector2>();

        rb.AddForce((transform.forward * moveInput.y) * acceleration);
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, speed);

        transform.Rotate(0, moveInput.x * minCameraRotationAmount, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

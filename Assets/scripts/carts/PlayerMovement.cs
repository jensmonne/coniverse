using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
    public float bumpForce = 1000f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private InputAction moveAction;

    private void Awake()
    {
        // Ensure PlayerInput and Action setup
        var playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Rigidbody setup
        rb.mass = 50f;
        rb.linearDamping = 0.5f; // Replaced linearDamping as Unity uses drag
        rb.angularDamping = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        MoveCart();
    }

    private void HandleInput()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        float move = inputVector.y;
        float turn = inputVector.x;

        if (move != 0)
        {
            moveDirection = transform.forward * (move * acceleration);
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        transform.Rotate(0, turn * rotationSpeed * Time.deltaTime, 0);
    }

    private void MoveCart()
    {
        if (moveDirection != Vector3.zero && rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BumperCart"))
        {
            Vector3 bumpDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bumpDirection * bumpForce, ForceMode.Impulse);
        }
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CartBehaviour : MonoBehaviour
{
    public float acceleration = 20f;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
    public float friction = 0.98f;
    public float bounceForce = 500f; // Small bounce effect when colliding

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 50f; // Reduced mass for a more rolling effect
        rb.linearDamping = 0.5f;
        rb.angularDamping = 1f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        MoveCart();
        ApplyFriction();
    }

    void HandleInput()
    {
        float move = Input.GetAxis("Vertical"); // Forward and backward
        float turn = Input.GetAxis("Horizontal"); // Left and right

        // Apply forward acceleration
        if (move != 0)
        {
            moveDirection = transform.forward * move * acceleration;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Apply rotation
        transform.Rotate(0, turn * rotationSpeed * Time.deltaTime, 0);
    }

    void MoveCart()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection, ForceMode.Acceleration);
        }

        // Limit max speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void ApplyFriction()
    {
        rb.linearVelocity *= friction;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bounce off other carts with a small knockback effect
        if (collision.gameObject.CompareTag("BumperCart"))
        {
            Vector3 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CartBehaviour : MonoBehaviour
{
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float friction = 0.98f;
    [SerializeField] private float bounceForce = 500f;

    private Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 50f;
        rb.linearDamping = 0.5f;
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
        ApplyFriction();
    }

    private void HandleInput()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

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
        if (moveDirection != Vector3.zero)
        {
            rb.AddForce(moveDirection, ForceMode.Acceleration);
        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void ApplyFriction()
    {
        rb.linearVelocity *= friction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colided");
        if (collision.gameObject.CompareTag("BumperCart"))
        {
            Debug.Log("BumperCart");
            Vector3 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 75f;
    public float XRotation = -90f;
    private float currentYRotation;

    private void FixedUpdate()
    {
        // You spin me right round till i die >:D
        currentYRotation += rotationSpeed * Time.deltaTime;

        if (currentYRotation >= 360f)
        {
            currentYRotation -= 360f;
        }

        transform.rotation = Quaternion.Euler(XRotation, currentYRotation, 0f);
    }
}

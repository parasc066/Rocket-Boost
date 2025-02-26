
using UnityEngine;

public class OscillatorWithRotation : MonoBehaviour
{
    [SerializeField] float speed; // Speed of movement
    [SerializeField] Vector3 movementVector; // Direction and distance to move

    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    float previousMovementFactor; // Track previous position in the ping-pong
    bool isMovingTowardsEnd = true; // Track movement direction

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;

        // Face the end position at start
        transform.forward = (endPosition - startPosition).normalized;
        previousMovementFactor = 0f;
    }

    void Update()
    {
        // Calculate movement factor (0 to 1)
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);

        // Check if direction changed
        bool movingTowardsEnd = movementFactor > previousMovementFactor;

        // Rotate only when direction changes
        if (movingTowardsEnd != isMovingTowardsEnd)
        {
            isMovingTowardsEnd = movingTowardsEnd;

            // Face the new direction
            Vector3 newDirection = isMovingTowardsEnd ?
                (endPosition - startPosition) :
                (startPosition - endPosition);

            transform.forward = newDirection.normalized;
        }

        // Update position and previous factor
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
        previousMovementFactor = movementFactor;
    }
}
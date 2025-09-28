using UnityEngine;
using UnityEngine.InputSystem; // New Input System

public class TankController : MonoBehaviour
{
    public Transform currentTank;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;      // conventional: movement speed
    public float rotationSpeed = 180f; // conventional: degrees per second

    private Vector2 moveInput;        // user-defined: stores WASD/Stick input
    private Rigidbody2D rb;           // conventional: tank body

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called from Input System "Move" action
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Forward/back movement (Y axis of input)
        float moveAmount = moveInput.y * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + (Vector2)transform.up * moveAmount);

        // Rotation (X axis of input)
        float rotationAmount = -moveInput.x * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }
}

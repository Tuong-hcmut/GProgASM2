
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    // --- THÊM BIẾN KIỂM SOÁT INPUT ---
    [Header("Input Setup")]
    public bool useArrowKeys = false; // true = Mũi tên, false = WASD (Default)
    // ---------------------------------

    [Header("Movement Settings (Arcade Top-Down)")]
    public float maxSpeed = 50f;            // Tốc độ tối đa
    public float acceleration = 30f;       // Lực đẩy khi nhấn nút

    [Header("Drift & Steering")]
    public float steerSpeed = 360f;        // Tốc độ xoay thân xe
    [Range(0f, 1f)]
    public float driftFactor = 0.95f;      // Hệ số giữ vận tốc ngang
    [Range(0f, 1f)]
    public float turnDriftFactor = 0.8f;   // Hệ số giữ vận tốc ngang khi lái gấp

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private float targetAngle;
    private bool isMoving;

    void Reset()
    {
        maxSpeed = 50f;
        acceleration = 30f;
        steerSpeed = 360f;
        driftFactor = 0.95f;
        turnDriftFactor = 0.8f;

        // Setup Rigidbody cho Reset
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.linearDamping = 0.1f; // Giá trị damping thấp cho quán tính
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.linearDamping = 0.1f; // Damping thấp cho quán tính
    }

    void Update()
    {
        // --- 1. Xử lý Input WASD/Mũi tên dựa trên biến useArrowKeys ---
        moveInput = Vector2.zero;
        var keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (!useArrowKeys)
            {
                // Input cho Player 1 (WASD)
                if (keyboard.wKey.isPressed) moveInput.y = 1f;
                if (keyboard.sKey.isPressed) moveInput.y = -1f;
                if (keyboard.aKey.isPressed) moveInput.x = -1f;
                if (keyboard.dKey.isPressed) moveInput.x = 1f;
            }
            else
            {
                // Input cho Player 2 (Mũi tên)
                if (keyboard.upArrowKey.isPressed) moveInput.y = 1f;
                if (keyboard.downArrowKey.isPressed) moveInput.y = -1f;
                if (keyboard.leftArrowKey.isPressed) moveInput.x = -1f;
                if (keyboard.rightArrowKey.isPressed) moveInput.x = 1f;
            }
        }

        moveInput.Normalize();
        isMoving = moveInput != Vector2.zero;

        // --- 2. Tính Toán Góc Quay Mục Tiêu (giữ nguyên) ---
        if (isMoving)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
        }
    }

    // Các hàm FixedUpdate, ApplySteering, ApplyMovement, ApplyDrift giữ nguyên.
    void FixedUpdate()
    {
        ApplySteering();
        ApplyMovement();
        ApplyDrift();
    }
    // --- A. Xoay Thân Xe (Luôn xoay về hướng Input) ---
    private void ApplySteering()
    {
        if (isMoving)
        {
            float currentAngle = transform.eulerAngles.z;
            // Xoay mượt về targetAngle
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, steerSpeed * Time.fixedDeltaTime);
            rb.rotation = newAngle; // Dùng rb.rotation thay vì transform.rotation
        }
    }

    // --- B. Áp Dụng Lực (Dựa trên Input) ---
    private void ApplyMovement()
    {
        if (isMoving)
        {
            // Áp dụng lực theo hướng INPUT (moveInput)
            Vector2 force = moveInput * acceleration;
            rb.AddForce(force, ForceMode2D.Force);

            // Giới hạn tốc độ
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            // Tăng damping khi không nhấn nút để xe dừng lại mượt hơn
            // (rb.linearDamping đã set sẵn trong Awake, có thể bỏ qua bước này nếu linearDamping đã đủ)
        }
    }

    // --- C. Áp Dụng Drift & Tạo Vòng Cung ---
    private void ApplyDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetAngle));

        // Logic này đảm bảo khi góc lệch lớn (đổi hướng gấp), turnDriftFactor thấp sẽ được áp dụng
        float t = Mathf.InverseLerp(45f, 180f, angleDiff);
        float appliedDrift = Mathf.Lerp(driftFactor, turnDriftFactor, t);

        // Giảm vận tốc ngang (rightVelocity) -> Tăng trượt đuôi
        rightVelocity *= appliedDrift;

        rb.linearVelocity = forwardVelocity + rightVelocity;
    }
}
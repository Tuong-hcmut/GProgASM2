using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AIController : MonoBehaviour
{
    [Header("Target & Speed")]
    [Tooltip("Kéo đối tượng trái bóng vào đây.")]
    public Transform ballTarget;

    [Tooltip("Kiểm soát tốc độ phản ứng của AI. Giá trị nhỏ hơn tạo ra sự chậm trễ.")]
    public float responseSpeed = 5f;

    [Tooltip("Khoảng cách mà AI sẽ dừng lại gần bóng.")]
    public float stopDistance = 0.5f;

    // Các biến vật lý của xe (lấy từ PlayerController2D)
    [Header("Physics Settings (Same as Player)")]
    public float maxSpeed = 50f;
    public float acceleration = 30f;
    public float steerSpeed = 360f;
    [Range(0f, 1f)]
    public float driftFactor = 0.95f;
    [Range(0f, 1f)]
    public float turnDriftFactor = 0.8f;
    public float linearDamping = 0.1f;

    private Rigidbody2D rb;
    private Vector2 moveInput; // Dùng để mô phỏng Input WASD
    private float targetAngle;
    private bool isMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.linearDamping = linearDamping;

        if (ballTarget == null)
        {
            // Cố gắng tìm bóng nếu chưa gán
            GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");
            if (ballObj != null) ballTarget = ballObj.transform;
        }
    }

    void Update()
    {
        if (ballTarget == null) return;

        // 1. TÍNH TOÁN HƯỚNG ĐI MỤC TIÊU
        Vector3 targetDirection = ballTarget.position - transform.position;
        float distance = targetDirection.magnitude;

        // 2. MÔ PHỎNG INPUT (moveInput)
        // Nếu ở quá gần bóng, AI sẽ không di chuyển
        if (distance > stopDistance)
        {
            // Lấy vector chỉ hướng (normalized)
            Vector2 desiredDirection = targetDirection.normalized;

            // Làm mượt input (responseSpeed) để AI không phản ứng quá nhanh
            moveInput = Vector2.Lerp(moveInput, desiredDirection, Time.deltaTime * responseSpeed);

            isMoving = true;
        }
        else
        {
            moveInput = Vector2.zero;
            isMoving = false;
        }

        // 3. TÍNH TOÁN GÓC QUAY MỤC TIÊU (Giống như PlayerController2D)
        if (isMoving)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
        }
    }

    void FixedUpdate()
    {
        // Sử dụng lại các hàm vật lý của PlayerController2D
        ApplySteering();
        ApplyMovement();
        ApplyDrift();
    }

    // --- CÁC HÀM VẬT LÝ ĐƯỢC SAO CHÉP TỪ PlayerController2D ---

    private void ApplySteering()
    {
        if (isMoving)
        {
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, steerSpeed * Time.fixedDeltaTime);
            rb.rotation = newAngle;
        }
    }

    private void ApplyMovement()
    {
        if (isMoving)
        {
            Vector2 force = moveInput * acceleration;
            rb.AddForce(force, ForceMode2D.Force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        // Để xe AI có quán tính tốt, không cần code dừng lại khi không input
    }

    private void ApplyDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        // Tính toán độ lệch góc giữa Hướng xe và Hướng input
        float angleDiff = Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetAngle));

        // Logic drift/đánh vòng cung
        float t = Mathf.InverseLerp(45f, 180f, angleDiff);
        float appliedDrift = Mathf.Lerp(driftFactor, turnDriftFactor, t);

        rightVelocity *= appliedDrift;
        rb.linearVelocity = forwardVelocity + rightVelocity;
    }
}
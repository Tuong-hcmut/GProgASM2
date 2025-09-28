using UnityEngine;

public class CarScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Movement")]
    public float moveSpeed = 8f;        // tốc độ di chuyển cơ bản
    public float rotationSpeed = 200f;  // tốc độ xoay (độ/giây)

    [Header("Boost")]
    public KeyCode boostKey = KeyCode.Space;
    public float boostMultiplier = 2f;   // nhân tốc khi boost
    public float boostDuration = 1f;     // thời gian boost (giây)
    public float boostCooldown = 3f;     // thời gian hồi (giây)

    [Header("Ball Interaction")]
    public float hitForce = 5f;          // lực đẩy bóng khi va chạm

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;

    // boost state
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float nextBoostTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // game 2D top-down
        rb.linearDamping = 1f;
    }

    void Update()
    {
        // tank control: WASD
        moveInput = 0f;
        turnInput = 0f;

        if (Input.GetKey(KeyCode.W)) moveInput = -1f;
        if (Input.GetKey(KeyCode.S)) moveInput = 1f;
        if (Input.GetKey(KeyCode.A)) turnInput = 1f;
        if (Input.GetKey(KeyCode.D)) turnInput = -1f;

        // boost logic
        if (Input.GetKeyDown(boostKey) && Time.time >= nextBoostTime)
        {
            isBoosting = true;
            boostEndTime = Time.time + boostDuration;
            nextBoostTime = Time.time + boostCooldown;
        }

        // stop boost if time over
        if (isBoosting && Time.time > boostEndTime)
            isBoosting = false;
    }

    void FixedUpdate()
    {
        // di chuyển
        float currentSpeed = moveSpeed;
        if (isBoosting) currentSpeed *= boostMultiplier;

        Vector2 forward = transform.up * moveInput * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forward);

        // xoay
        float rotation = turnInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                Vector2 dir = (collision.transform.position - transform.position).normalized;
                ballRb.AddForce(dir * hitForce, ForceMode2D.Impulse);

                // nếu đang boost thì cộng thêm lực
                if (isBoosting)
                    ballRb.AddForce(dir * hitForce * 0.5f, ForceMode2D.Impulse);
            }
        }
    }
}

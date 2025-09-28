using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallController2D : MonoBehaviour
{
    [Header("Physics")]
    public float maxSpeed = 20f;              // tốc độ tối đa
    public PhysicsMaterial2D bouncyMaterial;  // để bóng nảy khi chạm tường

    [Header("Respawn")]
    public Transform spawnPoint;              // vị trí reset bóng
    public float respawnDelay = 1.5f;         // delay sau khi ghi bàn

    private Rigidbody2D rb;
    private Collider2D col;

    private Vector2 startPosition;            // vị trí ban đầu của bóng

    // Start: chạy 1 lần khi bắt đầu
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // setup Rigidbody
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.linearDamping = 0.05f;
        rb.angularDamping = 0.05f;

        // gán physics material cho bóng
        if (bouncyMaterial != null)
            col.sharedMaterial = bouncyMaterial;

        // // spawn bóng ở vị trí giữa sân
        // if (spawnPoint != null)
        //     transform.position = spawnPoint.position;
        startPosition = transform.position;
    }

    // Update: chạy mỗi frame
    void Update()
    {
        // nếu muốn test reset nhanh bằng phím R
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(RespawnBall());
        }
    }

    // FixedUpdate: dùng cho physics
    void FixedUpdate()
    {
        // giới hạn tốc độ tối đa
        if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        rb.linearVelocity *= 0.99f;
    }

    // Hàm respawn bóng
    IEnumerator RespawnBall()
    {
        // disable chuyển động
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        // gameObject.SetActive(false);

        col.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // chờ delay
        yield return new WaitForSeconds(respawnDelay);

        // // reset vị trí
        // if (spawnPoint != null)
        //     transform.position = spawnPoint.position;
        // else
        //     transform.position = Vector2.zero;

        // gameObject.SetActive(true);
        // reset về vị trí ban đầu
        transform.position = startPosition;

        col.enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}

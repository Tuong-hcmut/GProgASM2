using UnityEngine;
using UnityEngine.UI;

public class TankHUD : MonoBehaviour
{
    [Header("References")]
    public Transform ball;            // The ball in the scene
    public Camera cam;                // Main camera
    public GameObject offscreenArrow; // Arrow prefab/UI
    public Text distanceText;         // UI text for distance
    public RectTransform compassNeedle; // Needle UI for orientation

    private Renderer rd; // To check if tank is on screen

    void Start()
    {
        rd = GetComponent<Renderer>();
        offscreenArrow.SetActive(false);
    }

    void Update()
    {
        HandleDistance();
        HandleOrientation();
        HandleOffscreenArrow();
    }

    // --- Show distance to ball ---
    void HandleDistance()
    {
        float dist = Vector3.Distance(transform.position, ball.position);
        distanceText.text = $"{dist:F1}m";
    }

    // --- Show orientation relative to play area ---
    void HandleOrientation()
    {
        // Forward is assumed to be +Y in 2D, or +Z in 3D
        Vector3 forward = transform.up;
        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        compassNeedle.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    // --- Offscreen indicator around the ball ---
    void HandleOffscreenArrow()
    {
        if (rd.isVisible)
        {
            if (offscreenArrow.activeSelf)
                offscreenArrow.SetActive(false);
        }
        else
        {
            if (!offscreenArrow.activeSelf)
                offscreenArrow.SetActive(true);

            // Direction from ball to this tank
            Vector3 dir = (transform.position - ball.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Circle around the ball’s screen position
            Vector3 ballScreen = cam.WorldToScreenPoint(ball.position);
            float radius = 100f; // distance from ball in UI pixels
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
                                         Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            offscreenArrow.transform.position = ballScreen + offset;
            offscreenArrow.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}

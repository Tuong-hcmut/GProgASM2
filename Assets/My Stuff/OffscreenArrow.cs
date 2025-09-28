using UnityEngine;
using UnityEngine.UI;

public class OffscreenArrow : MonoBehaviour {
    public Transform target;      // Tank being tracked (user-defined)
    public Transform ball;        // Fixed: Ball
    public RectTransform arrowUI; // UI arrow image
    public Camera cam;            // Main Camera

    void Update() {
        Vector3 screenPos = cam.WorldToViewportPoint(target.position);

        bool offscreen = screenPos.x < 0 || screenPos.x > 1 ||
                         screenPos.y < 0 || screenPos.y > 1 ||
                         screenPos.z < 0;

        arrowUI.gameObject.SetActive(offscreen);

        if (offscreen) {
            Vector3 dir = (target.position - ball.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Circle around ball’s screen position
            Vector3 ballScreen = cam.WorldToScreenPoint(ball.position);
            float radius = 100f; // how far from ball the arrow sits
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),
                                         Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            arrowUI.position = ballScreen + offset;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
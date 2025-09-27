using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArrowManager : MonoBehaviour
{
    public Camera mainCamera;
    public Transform ball;
    public RectTransform arrowPrefab; // assign prefab in Inspector
    public Canvas canvas;
    public float radius = 100f; // distance from ball in UI units

    private Dictionary<Transform, RectTransform> tankArrows = new();

    public void RegisterTank(Transform tank, Color color)
    {
        RectTransform arrow = Instantiate(arrowPrefab, canvas.transform);
        arrow.GetComponent<Image>().color = color;
        arrow.gameObject.SetActive(false);
        tankArrows[tank] = arrow;
    }

    void Update()
    {
        if (ball == null || mainCamera == null) return;

        Vector3 ballScreen = mainCamera.WorldToScreenPoint(ball.position);

        foreach (var pair in tankArrows)
        {
            Transform tank = pair.Key;
            RectTransform arrow = pair.Value;

            Vector3 tankScreen = mainCamera.WorldToScreenPoint(tank.position);

            if (IsOnScreen(tankScreen))
            {
                arrow.gameObject.SetActive(false);
                continue;
            }

            // direction from ball to tank
            Vector3 dir = (tankScreen - ballScreen).normalized;

            // arrow position on circle around ball
            Vector3 arrowScreen = ballScreen + dir * radius;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                arrowScreen, mainCamera, out Vector2 localPoint);

            arrow.localPosition = localPoint;

            // rotate arrow
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.Euler(0, 0, angle - 90f);

            arrow.gameObject.SetActive(true);
        }
    }

    private bool IsOnScreen(Vector3 screenPos)
    {
        return screenPos.x >= 0 && screenPos.x <= Screen.width &&
               screenPos.y >= 0 && screenPos.y <= Screen.height &&
               screenPos.z > 0;
    }
}

using UnityEngine;

public class CameraFollowClamp2D : MonoBehaviour
{
    [Header("Follow Target")]
    public Transform target; // assign your player

    [Header("Clamp Area (Rectangle Object)")]
    public Transform playAreaObject; // assign your rectangle or BoxCollider2D

    private Camera mainCam;
    private Vector2 areaSize;

    void Awake()
    {
        mainCam = Camera.main;

        if (playAreaObject != null)
        {
            // Try to get size from BoxCollider2D
            var collider = playAreaObject.GetComponent<BoxCollider2D>();
            if (collider != null)
                areaSize = collider.size * playAreaObject.lossyScale;
            else
                areaSize = playAreaObject.localScale;
        }
    }

    void LateUpdate()
    {
        if (target == null || mainCam == null || playAreaObject == null) return;

        // Camera size in world units
        float camHeight = 2f * mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        // Play area center
        Vector2 areaCenter = playAreaObject.position;

        // Clamp bounds
        float minX = areaCenter.x - areaSize.x / 2f + camWidth / 2f;
        float maxX = areaCenter.x + areaSize.x / 2f - camWidth / 2f;
        float minY = areaCenter.y - areaSize.y / 2f + camHeight / 2f;
        float maxY = areaCenter.y + areaSize.y / 2f - camHeight / 2f;

        // Clamp target position
        Vector3 desiredPos = target.position;
        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.y = Mathf.Clamp(desiredPos.y, minY, maxY);

        // Apply to camera (keep original z)
        mainCam.transform.position = new Vector3(
            desiredPos.x, desiredPos.y, mainCam.transform.position.z
        );
    }
}

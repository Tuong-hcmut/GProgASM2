using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayAreaController : MonoBehaviour
{
    [SerializeField] private float safeOffset = 0.5f; // user-defined: how far inside the border to place objects

    private Collider2D areaCollider;

    private void Awake()
    {
        areaCollider = GetComponent<Collider2D>();
        areaCollider.isTrigger = true; // ensure it's a trigger
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        Vector2 center = areaCollider.bounds.center;
        Vector2 fromCenterToObj = (Vector2)other.transform.position - center;

        // Find intersection point along this direction
        Vector2 newPos = FindBoundaryIntersection(center, fromCenterToObj.normalized);

        // Move slightly inward
        newPos -= fromCenterToObj.normalized * safeOffset;

        // Teleport object
        rb.position = newPos;
        rb.linearVelocity = Vector2.zero; // optional: stop motion to prevent immediate re-exit
        Debug.Log($"{other.name} exited bounds — teleported back inside play area.");
    }

    private Vector2 FindBoundaryIntersection(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity, LayerMask.GetMask("PlayAreaEdge"));
        if (hit.collider != null)
        {
            return hit.point;
        }

        // Fallback: approximate with bounding box if no hit
        Bounds b = areaCollider.bounds;
        Vector2 max = b.max;
        Vector2 min = b.min;
        Vector2 end = origin + direction * 100f; // far away

        // Clamp to box edges
        float tX = direction.x > 0 ? (max.x - origin.x) / direction.x : (min.x - origin.x) / direction.x;
        float tY = direction.y > 0 ? (max.y - origin.y) / direction.y : (min.y - origin.y) / direction.y;

        float t = Mathf.Min(tX, tY);
        return origin + direction * t;
    }
}
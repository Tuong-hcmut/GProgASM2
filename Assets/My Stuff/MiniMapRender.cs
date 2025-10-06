using UnityEngine;
using UnityEngine.UI;

public class MiniMapRender : MonoBehaviour
{
    [Header("References")]
    public Transform ball;                 // user-defined
    public Transform playAreaObject;       // user-defined

    private Vector2 areaSize;
    private Vector2 areaCenter;
    private RawImage icon;                 // the Image attached to this object

    [Header("Visibility Settings")]
    [Range(0f, 1f)]
    public float edgePercent = 0.3f;       // conventional (30%)

    public enum HideRegion { Top, Bottom } // fixed
    public HideRegion hideRegion = HideRegion.Top; // user-defined

    void Awake()
    {
        icon = GetComponent<RawImage>();

        if (playAreaObject != null)
        {
            // Get area size from BoxCollider2D if possible
            var collider = playAreaObject.GetComponent<BoxCollider2D>();
            if (collider != null)
                areaSize = collider.size * playAreaObject.lossyScale;
            else
                areaSize = playAreaObject.localScale;

            areaCenter = playAreaObject.position;
        }
    }

    void Update()
    {
        if (ball == null || icon == null || playAreaObject == null) return;

        float fieldTopY = areaCenter.y + (areaSize.y / 2f);
        float fieldBottomY = areaCenter.y - (areaSize.y / 2f);
        float totalHeight = fieldTopY - fieldBottomY;

        bool shouldShow = true;

        if (hideRegion == HideRegion.Top)
        {
            float topThresholdY = fieldTopY - (totalHeight * edgePercent);
            shouldShow = ball.position.y < topThresholdY;
        }
        else // HideRegion.Bottom
        {
            float bottomThresholdY = fieldBottomY + (totalHeight * edgePercent);
            shouldShow = ball.position.y > bottomThresholdY;
        }

        icon.enabled = shouldShow;
    }
}

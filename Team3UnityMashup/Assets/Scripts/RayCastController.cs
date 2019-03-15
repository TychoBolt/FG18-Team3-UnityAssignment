using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RayCastController : MonoBehaviour
{
    public struct RayCastOrigins
    {
        public Vector2 TopLeft, TopRight;
        public Vector2 BottomLeft, BottomRight;
    }

    public const float SkinWidth = 0.015f;
    private const float DistanceBetweenRays = 0.25f;

    [HideInInspector]
    public int HorizontalRayCount;
    [HideInInspector]
    public int VerticalRayCount;

    [Header("Mask for 'Ground', Also needs to be set in Controller2D script")]
    public LayerMask CollisionMask;

    [HideInInspector]
    public float HorizontalRaySpacing;
    [HideInInspector]
    public float VerticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D BoxCollider;
    public RayCastOrigins RayCastOriginsCorners;

    public virtual void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void UpdateRayCastOrigins()
    {
        Bounds BoxBounds = BoxCollider.bounds;
        BoxBounds.Expand(SkinWidth * -2);

        RayCastOriginsCorners.BottomLeft = new Vector2(BoxBounds.min.x, BoxBounds.min.y);
        RayCastOriginsCorners.BottomRight = new Vector2(BoxBounds.max.x, BoxBounds.min.y);
        RayCastOriginsCorners.TopLeft = new Vector2(BoxBounds.min.x, BoxBounds.max.y);
        RayCastOriginsCorners.TopRight = new Vector2(BoxBounds.max.x, BoxBounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds BoxBounds = BoxCollider.bounds;
        BoxBounds.Expand(SkinWidth * -2);

        float BoundsWidth = BoxBounds.size.x;
        float BoundsHeight = BoxBounds.size.y;

        HorizontalRayCount = Mathf.RoundToInt(BoundsHeight / DistanceBetweenRays);
        VerticalRayCount = Mathf.RoundToInt(BoundsWidth / DistanceBetweenRays);

        HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);

        HorizontalRaySpacing = BoxBounds.size.y / (HorizontalRayCount - 1);
        VerticalRaySpacing = BoxBounds.size.x / (VerticalRayCount - 1);
    }
}

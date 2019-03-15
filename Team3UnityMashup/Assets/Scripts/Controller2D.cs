using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : RayCastController
{
    public struct CollisionInfo
    {
        public bool Above, Below;
        public bool Left, Right;
        public bool FallingThroughPlatform;

        public bool ClimbingSlope;
        public bool DescendingSlope;
        public bool SlidingDownMaxSlope;
        public float SlopeAngle, SlopeAngleOld;

        public int FaceDirection;
        public Vector2 Dir;

        public Vector2 DeltaMoveOld;
        public Vector2 SlopeNormal;

        public void Reset()
        {
            Above = Below = false;
            Left = Right = false;
            ClimbingSlope = false;
            DescendingSlope = false;
            SlidingDownMaxSlope = false;

            SlopeNormal = Vector2.zero;

            SlopeAngleOld = SlopeAngle;
            SlopeAngle = 0;
        }
    }

    [HideInInspector]
    [Header("Angle for going up/down slopes")]
    public float MaxSlopeAngle = 50;

    public CollisionInfo Collisions;
    Vector2 PlayerInput;

    [Header("Click on the right circle and add main character")]
    [Tooltip("This is for fliping the character, for now.")]
    public SpriteRenderer PlayerSprite;

    void ResetFallingThroughPlatform()
    {
        Collisions.FallingThroughPlatform = false;
    }

    public override void Start()
    {
        base.Start();
        Collisions.FaceDirection = 1;
    }

    public void Move(Vector2 DeltaMove, Vector2 _PlayerInput, bool StandingOnPlatform = false)
    {
        UpdateRayCastOrigins();
        Collisions.Reset();
        Collisions.DeltaMoveOld = DeltaMove;
        PlayerInput = _PlayerInput;

        if (DeltaMove.y < 0)
        {
            DescendSlope(ref DeltaMove);
        }

        if (DeltaMove.x != 0)
        {
            Collisions.FaceDirection = (int)Mathf.Sign(DeltaMove.x);
            Collisions.Dir = new Vector2(PlayerInput.x, PlayerInput.y);

            if (Collisions.Dir.x > 0)
            {
                PlayerSprite.flipX = false;
            }
            else if (Collisions.Dir.x < 0)
            {
                PlayerSprite.flipX = true;
            }

        }

        HorizontalCollision(ref DeltaMove);

        if (DeltaMove.y != 0)
        {
            VerticalCollision(ref DeltaMove);
        }

        if (StandingOnPlatform)
        {
            Collisions.Below = true;
        }

        transform.Translate(DeltaMove);
    }

    private void HorizontalCollision(ref Vector2 DeltaMove)
    {
        float DirectionX = Collisions.FaceDirection;
        float RayLength = Mathf.Abs(DeltaMove.x) + SkinWidth;

        if (Mathf.Abs(DeltaMove.x) < SkinWidth)
        {
            RayLength = SkinWidth * 2;
        }

        for (int i = 0; i < HorizontalRayCount; i++)
        {
            Vector2 RayOrigin = (DirectionX == -1) ? RayCastOriginsCorners.BottomLeft : RayCastOriginsCorners.BottomRight;
            RayOrigin += Vector2.up * (HorizontalRaySpacing * i);
            RaycastHit2D RayCastHit = Physics2D.Raycast(RayOrigin, Vector2.right * DirectionX, RayLength, CollisionMask);

            //Debug.DrawRay(RayOrigin, Vector2.right * DirectionX, Color.red); // Testing (Drawing the rays)

            if (RayCastHit)
            {
                if (RayCastHit.distance == 0)
                {
                    continue;
                }

                float SlopeAngle = Vector2.Angle(RayCastHit.normal, Vector2.up);

                if (i == 0 && SlopeAngle <= MaxSlopeAngle)
                {
                    if (Collisions.DescendingSlope)
                    {
                        Collisions.DescendingSlope = false;
                        DeltaMove = Collisions.DeltaMoveOld;
                    }

                    float DistanceToSlopeStart = 0;

                    if (SlopeAngle != Collisions.SlopeAngleOld)
                    {
                        DistanceToSlopeStart = RayCastHit.distance - SkinWidth;
                        DeltaMove.x -= DistanceToSlopeStart * DirectionX;
                    }

                    ClimbSlope(ref DeltaMove, SlopeAngle, RayCastHit.normal);
                    DeltaMove.x += DistanceToSlopeStart * DirectionX;
                }

                if (!Collisions.ClimbingSlope || SlopeAngle > MaxSlopeAngle)
                {
                    DeltaMove.x = (RayCastHit.distance - SkinWidth) * DirectionX;
                    RayLength = RayCastHit.distance;

                    if (Collisions.ClimbingSlope)
                    {
                        DeltaMove.y = Mathf.Tan(Collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(DeltaMove.x);
                    }

                    Collisions.Left = DirectionX == -1;
                    Collisions.Right = DirectionX == 1;
                }
            }
        }
    }

    private void VerticalCollision(ref Vector2 DeltaMove)
    {
        float DirectionY = Mathf.Sign(DeltaMove.y);
        float RayLength = Mathf.Abs(DeltaMove.y) + SkinWidth;

        for (int i = 0; i < VerticalRayCount; i++)
        {
            Vector2 RayOrigin = (DirectionY == -1) ? RayCastOriginsCorners.BottomLeft : RayCastOriginsCorners.TopLeft;
            RayOrigin += Vector2.right * (VerticalRaySpacing * i + DeltaMove.x);
            RaycastHit2D RayCastHit = Physics2D.Raycast(RayOrigin, Vector2.up * DirectionY, RayLength, CollisionMask);

            //Debug.DrawRay(RayOrigin, Vector2.up * DirectionY, Color.red); // Testing (Drawing the rays)

            if (RayCastHit)
            {
                if (RayCastHit.collider.CompareTag("Through"))
                {
                    if (DirectionY == 1 || RayCastHit.distance == 0)
                    {
                        continue;
                    }

                    if (Collisions.FallingThroughPlatform)
                    {
                        continue;
                    }

                    if (PlayerInput.y == -1)
                    {
                        Collisions.FallingThroughPlatform = true;
                        Invoke("ResetFallingThroughPlatform", 0.5f);
                        continue;
                    }
                }

                DeltaMove.y = (RayCastHit.distance - SkinWidth) * DirectionY;
                RayLength = RayCastHit.distance;

                if (Collisions.ClimbingSlope)
                {
                    DeltaMove.x = DeltaMove.y / Mathf.Tan(Collisions.SlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(DeltaMove.x);
                }

                Collisions.Below = DirectionY == -1;
                Collisions.Above = DirectionY == 1;
            }
        }

        if (Collisions.ClimbingSlope)
        {
            float DirectionX = Mathf.Sign(DeltaMove.x);

            RayLength = Mathf.Abs(DeltaMove.x) + SkinWidth;
            Vector2 RayOrigin = ((DirectionX == -1) ? RayCastOriginsCorners.BottomLeft : RayCastOriginsCorners.BottomRight) + Vector2.up * DeltaMove.y;
            RaycastHit2D RayCastHit = Physics2D.Raycast(RayOrigin, Vector2.right * DirectionX, RayLength, CollisionMask);

            if (RayCastHit)
            {
                float SlopeAngle = Vector2.Angle(RayCastHit.normal, Vector2.up);

                if (SlopeAngle != Collisions.SlopeAngle)
                {
                    DeltaMove.x = (RayCastHit.distance - SkinWidth) * DirectionX;
                    Collisions.SlopeAngle = SlopeAngle;
                    Collisions.SlopeNormal = RayCastHit.normal;
                }
            }
        }
    }

    private void DescendSlope(ref Vector2 DeltaMove)
    {
        RaycastHit2D MaxSlopeHitLeft = Physics2D.Raycast(RayCastOriginsCorners.BottomLeft, Vector2.down, Mathf.Abs(DeltaMove.y) + SkinWidth, CollisionMask);
        RaycastHit2D MaxSlopeHitRight = Physics2D.Raycast(RayCastOriginsCorners.BottomRight, Vector2.down, Mathf.Abs(DeltaMove.y) + SkinWidth, CollisionMask);

        if (MaxSlopeHitLeft ^ MaxSlopeHitRight)
        {
            SlideDownMaxSlope(MaxSlopeHitLeft, ref DeltaMove);
            SlideDownMaxSlope(MaxSlopeHitRight, ref DeltaMove);
        }


        if (!Collisions.SlidingDownMaxSlope)
        {
            float DirectionX = Mathf.Sign(DeltaMove.x);

            Vector2 RayOrigin = (DirectionX == 1) ? RayCastOriginsCorners.BottomRight : RayCastOriginsCorners.BottomLeft;
            RaycastHit2D RayCastHit = Physics2D.Raycast(RayOrigin, -Vector2.up, Mathf.Infinity, CollisionMask);

            if (RayCastHit)
            {
                float SlopeAngle = Vector2.Angle(RayCastHit.normal, Vector2.up);

                if (SlopeAngle != 0 && SlopeAngle <= MaxSlopeAngle)
                {
                    if (Mathf.Sign(RayCastHit.normal.x) == DirectionX)
                    {
                        if (RayCastHit.distance - SkinWidth <= Mathf.Tan(SlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(DeltaMove.x))
                        {
                            float MoveDistance = Mathf.Abs(DeltaMove.x);
                            float DescendDeltaMoveY = Mathf.Sin(SlopeAngle * Mathf.Deg2Rad) * MoveDistance;

                            DeltaMove.x = Mathf.Cos(SlopeAngle * Mathf.Deg2Rad) * MoveDistance;
                            DeltaMove.y -= DescendDeltaMoveY;

                            Collisions.SlopeAngle = SlopeAngle;
                            Collisions.DescendingSlope = true;
                            Collisions.Below = true;
                            Collisions.SlopeNormal = RayCastHit.normal;
                        }
                    }
                }
            }
        }
    }

    private void SlideDownMaxSlope(RaycastHit2D RayCastHit, ref Vector2 DeltaMove)
    {
        if (RayCastHit)
        {
            float SlopeAngle = Vector2.Angle(RayCastHit.normal, Vector2.up);
            if (SlopeAngle > MaxSlopeAngle)
            {
                DeltaMove.x = RayCastHit.normal.x * (Mathf.Abs(DeltaMove.y) - RayCastHit.distance) / Mathf.Tan(SlopeAngle * Mathf.Deg2Rad);

                Collisions.SlopeAngle = SlopeAngle;
                Collisions.SlidingDownMaxSlope = true;
                Collisions.SlopeNormal = RayCastHit.normal;
            }
        }
    }

    private void ClimbSlope(ref Vector2 DeltaMove, float SlopeAngle, Vector2 SlopeNormal)
    {
        float MoveDistance = Mathf.Abs(DeltaMove.x);
        float ClimbDeltaMoveY = Mathf.Sin(SlopeAngle * Mathf.Deg2Rad) * MoveDistance;

        if (DeltaMove.y <= ClimbDeltaMoveY)
        {
            DeltaMove.y = ClimbDeltaMoveY;
            DeltaMove.x = Mathf.Cos(SlopeAngle * Mathf.Deg2Rad) * MoveDistance * Mathf.Sign(DeltaMove.x);
            Collisions.Below = true;
            Collisions.ClimbingSlope = true;
            Collisions.SlopeAngle = SlopeAngle;
            Collisions.SlopeNormal = SlopeNormal;
        }
    }

    public Vector2 GetFaceDirection()
    {
        return Collisions.Dir;
    }
}

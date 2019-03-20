using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    [Header("Controller2D")]
    public Controller2D Controller;

    [Header("Jump settings")]
    public float MaxJumpHeight = 4f;
    public float MinJumpHeight = 1f;
    public float TimeToJumpApex = 0.4f;

    [Header("Move speed")]
    public float MoveSpeed = 6f;

    private Vector2 Velocity;

    [Header("Acceleration smoothing (Airborne & Ground)")]
    public float AccelerationTimeAirborne = 0.2f;
    public float AccelerationTimeGrounded = 0.1f;
    private float VelocityXSmoothing;

    private float MaxJumpVelocity;
    private float MinJumpVelocity;
    private float Gravity;

    private Vector2 DirectionalInput;
    private int WallDirectionX;
    private Animator animator;

    private float MaxGravity = -20f;

    private void Start()
    {
        Controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();

        Gravity = -((2 * MaxJumpHeight) / Mathf.Pow(TimeToJumpApex, 2));
        MaxJumpVelocity = Mathf.Abs(Gravity) * TimeToJumpApex;
        MinJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Gravity) * MinJumpHeight);
    }

    private void Update()
    {
        WallDirectionX = (Controller.Collisions.Left) ? -1 : 1;

        CalculateVelocity();
        
    }

   private void FixedUpdate()
    {
        Controller.Move(Velocity * Time.deltaTime, DirectionalInput);

        if (DirectionalInput.x > 0 || DirectionalInput.x < 0)
        {
            animator.SetBool("IsRunning?", true);
        }
        else
        {
            animator.SetBool("IsRunning?", false);
        }

       

    }

    public void SetDirectionalInput(Vector2 _DirectionalInput)
    {
        DirectionalInput = _DirectionalInput;
    }

    public void OnJumpInputDown()
    {
        animator.SetBool("IsJumpStart?", true);     //ALEX

        if (Controller.Collisions.Below)
        {
            Velocity.y = MaxJumpVelocity;
        }
    }

    public void OnJumpInputUp()
    {
        animator.SetBool("IsJumpStart?", false);
        if (Velocity.y > MinJumpVelocity)
        {
            Velocity.y = MinJumpVelocity;
        }
    }

    private void CalculateVelocity()
    {
        float TargetVelocityX = DirectionalInput.x * MoveSpeed;
        Velocity.x = Mathf.SmoothDamp(Velocity.x, TargetVelocityX, ref VelocityXSmoothing, (Controller.Collisions.Below) ? AccelerationTimeGrounded : AccelerationTimeAirborne);

        bool Landing = animator.GetBool("IsJumpFall?") ? true : false;
        bool HittingCeiling = (animator.GetBool("IsJumpStart?") && Controller.Collisions.Above) ? true : false;
        

        // We hit our head in the ceiling
        if (HittingCeiling)
        {
            // Player is in air so we will fall
            if (!Controller.Collisions.Below)
            {
                animator.SetBool("IsJumpFall?", true);

                Velocity.y += Gravity * Time.deltaTime;
                if (Velocity.y < MaxGravity)
                {
                    Velocity.y = MaxGravity;

                }
            }
        }

        // If we are falling increase velocity
        if (!Controller.Collisions.Below)
        {
            animator.SetBool("IsJumpFall?", true);

            Velocity.y += Gravity * Time.deltaTime;
            if (Velocity.y < MaxGravity)
            {
                Velocity.y = MaxGravity;

            }
        }
        else
        {
            if (Landing)
            {
                animator.SetBool("IsJumpLand?", true);
                animator.SetBool("IsJumpFall?", false);
            }

            if (!Landing)
            {
                animator.SetBool("IsJumpLand?", false);
            }
        }
    }
}

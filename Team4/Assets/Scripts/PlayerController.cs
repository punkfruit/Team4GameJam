using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction _layEggAction;
    private EggLayingArea currentEggLayingArea;

    public float hoverLift = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float speedLimit = 20.0f;
    public float bounceStrengthX = 40.0f;
    public float manualDropForce = 30.0f;
    public float maxFlightTime = 3.0f;
    public float flightTimeRecoveryRate = 0.5f;
    public float landedFlightTimeRecoveryMultiplier = 4.0f;

    [HideInInspector]
    public bool isControlActive = true;

    [SerializeField]
    private RectTransform flightMeter;
    public float flightMeterWidth;
    public float flightMeterHeight;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    float movementAxis;
    bool bIsHovering = false;
    bool bIsDropping = false;
    bool landedOnPlatform = false;
    float flightTimeRemaining = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _layEggAction = playerInput.actions["LayEgg"];
        _layEggAction?.Disable();//The ? is a null conditional operator, it checks if _layEggAction is not null before calling Disable() to avoid a NullReferenceException if the action is not found.
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flightTimeRemaining = maxFlightTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            //horizontal speed
            rb.AddForceX(horizontalSpeed * movementAxis);
            //lift/hover
            if (bIsHovering && flightTimeRemaining > 0)
            {
                rb.AddForceY(hoverLift);

                SetFlightTimeRemaining(flightTimeRemaining - Time.deltaTime);
            } else
            {
                float multiplier = landedOnPlatform ? landedFlightTimeRecoveryMultiplier : 1;
                SetFlightTimeRemaining(Mathf.Min(maxFlightTime, flightTimeRemaining + (Time.deltaTime * flightTimeRecoveryRate * multiplier)));
            } 
            
            if (bIsDropping && !bIsHovering) {
                rb.AddForce(Vector2.down * manualDropForce, ForceMode2D.Force);
            }

            if(rb.linearVelocity.magnitude > speedLimit)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            rb.AddForceX(-1 * bounceStrengthX * Mathf.Abs(normal.x) * horizontalSpeed * movementAxis);
            if (normal.y > 0)
            {
                landedOnPlatform = true;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
        } else if (collision.gameObject.CompareTag("PointTrigger"))
        {
            collision.gameObject.GetComponent<PointTrigger>().OnTrigger();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platforms"))
        {
            landedOnPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EggTriggerZone"))
        {
            _layEggAction?.Enable();
            //TODO: Show UI prompt to indicate the player can lay an egg
            currentEggLayingArea = collision.GetComponent<EggLayingArea>();
        }
        if (collision.CompareTag("Goal"))
        {
            GameDirector.Instance.GoalReached();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EggTriggerZone"))
        {
            _layEggAction?.Disable();
            //Hide UI prompt to indicate the player can no longer lay an egg
            currentEggLayingArea = null;
        }
    }

    public void OnLayEgg(InputAction.CallbackContext context)
    {
        if (currentEggLayingArea != null)
        {
            currentEggLayingArea.isBeingFilled = context.ReadValueAsButton();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = context.ReadValue<float>();
        if (bIsDropping)
        {
            return;
        }
        if (movementAxis > 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementAxis < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void OnHover(InputAction.CallbackContext context)
    {
        bIsHovering = context.ReadValueAsButton();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        if (bIsHovering)
        {
            bIsDropping = false;
            return;
        }
        bIsDropping = context.ReadValueAsButton();
        if (bIsDropping && !landedOnPlatform)
        {
            float angle = spriteRenderer.flipX ? 270f : 90f;
            transform.eulerAngles = new Vector3(0f, 0f, angle);
        } else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    private void SetFlightTimeRemaining(float newFlightTime)
    {
        flightTimeRemaining = newFlightTime;
        float newWidth = (newFlightTime / maxFlightTime) * flightMeterWidth;
        flightMeter.sizeDelta = new Vector2(newWidth, flightMeterHeight);
    }
}

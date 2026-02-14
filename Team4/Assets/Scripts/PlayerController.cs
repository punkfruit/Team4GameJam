using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float hoverLift = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float speedLimit = 20.0f;
    public float bounceStrengthX = 40.0f;
    public float manualDropForce = 30.0f;
    public float maxFlightTime = 3.0f;
    public float flightTimeRecoveryRate = 0.5f;

    [SerializeField]
    private RectTransform flightMeter;
    public float flightMeterWidth;
    public float flightMeterHeight;

    Rigidbody2D rb;
    float movementAxis;
    bool bIsHovering = false;
    bool bIsDropping = false;
    float flightTimeRemaining = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flightTimeRemaining = maxFlightTime;
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
                SetFlightTimeRemaining(Mathf.Min(maxFlightTime, flightTimeRemaining + (Time.deltaTime * flightTimeRecoveryRate)));
            } 
            
            if (bIsDropping) {
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
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        rb.AddForceX(-1 * bounceStrengthX * Mathf.Abs(normal.x) * horizontalSpeed * movementAxis);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = context.ReadValue<float>();
    }

    public void OnHover(InputAction.CallbackContext context)
    {
        bIsHovering = context.ReadValueAsButton();
    }

    public void OnDrop(InputAction.CallbackContext context)
    {
        bIsDropping = context.ReadValueAsButton();
    }

    private void SetFlightTimeRemaining(float newFlightTime)
    {
        flightTimeRemaining = newFlightTime;
        float newWidth = (newFlightTime / maxFlightTime) * flightMeterWidth;
        flightMeter.sizeDelta = new Vector2(newWidth, flightMeterHeight);
    }
}

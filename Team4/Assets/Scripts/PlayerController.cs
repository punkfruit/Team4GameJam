using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float hoverLift = 5.0f;
    public float horizontalSpeed = 2.0f;
    public float speedLimit = 20.0f;

    Rigidbody2D rb;
    float movementAxis;
    bool bIsHovering = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            //horizontal speed
            rb.AddForceX(horizontalSpeed * movementAxis);
            //lift/hover
            if (bIsHovering)
            {
                rb.AddForceY(hoverLift);
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

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = context.ReadValue<float>();
    }

    public void OnHover(InputAction.CallbackContext context)
    {
        bIsHovering = context.ReadValueAsButton();
    }
}

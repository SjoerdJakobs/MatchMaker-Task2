using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpStrenght = 1.5f;
    [SerializeField]
    private float characterSize = 1f;//groote van 1 cube
    [SerializeField]
    private float maxSpeedChange = 8f;
    [SerializeField]
    private float gravityStrenght = 9.81f;
    private float groundRayRange;

    [SerializeField]
    private bool airControl = false;
    private bool canJump;
    private bool grounded;
    [SerializeField]
    public bool _useGravity = true;

    private Rigidbody rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        groundRayRange = (characterSize / 2);
        canJump = true;
    }

    void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position, new Vector3(groundRayRange, 0.1f, groundRayRange), Vector3.down, transform.rotation, groundRayRange))
        {
            grounded = true;
            canJump = true;
        }
        else
        {
            grounded = false;
            canJump = false;
        }

        if (grounded || airControl)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rigid.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxSpeedChange, maxSpeedChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxSpeedChange, maxSpeedChange);
            velocityChange.y = 0;
            rigid.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                jump(velocity);
            }
        }
        if (_useGravity)
        {
            rigid.AddForce(new Vector3(0, -gravityStrenght * rigid.mass, 0));
        }
        //grounded = false;
    }
    public void jump(Vector3 velocity)
    {
        //print("testc");
        rigid.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
        canJump = false;
    }
    float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2 * jumpStrenght * gravityStrenght);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public bool canMoveInDiagonal;
    public bool canJump;
    public bool canStopMove;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

/*    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;*/

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    public float horizontalInput;
    float verticalInput;


    Vector3 moveDirection;

    Rigidbody rb;

    [SerializeField] InputGame _playerInput;
    private Vector2 _movementInput;
    bool _jumpButton;

    [SerializeField] bool isPlayer2;

    private void Start()
    {
        //var p = FindObjectOfType<PlayerInputManager>().JoinPlayer(isPlayer2, -1, isPlayer2==1?"Keyboard&Mouse":"ArrowKeyBoard");
        //p.currentActionMap["Move"].started += PlayerMovement_started;
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;

        readyToJump = true;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isPlayer2 == true) return;
        if (context.phase != InputActionPhase.Started) return;

        _movementInput = context.ReadValue<Vector2>();
        //if ( !canMoveInDiagonal)
        {
            Debug.Log(_movementInput);
            transform.Rotate(0, 90f * _movementInput.x, 0);
        }
    }


    private void Update()
    {
        if(isPlayer2)
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                _movementInput.x = 1f;
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _movementInput.x = -1f;
            }
            else
            {
                _movementInput.x = 0f;
            }
            transform.Rotate(0, 90f * _movementInput.x, 0);
        }

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        MovePlayer();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void MyInput()
    {
        //if (canMoveInDiagonal)
        //{
        //    horizontalInput = _movementInput.x;
        //    verticalInput = _movementInput.y;
        //}

        // when to jump
        if (_jumpButton && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();
            

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        //if (canMoveInDiagonal)
        //{
        //    // calculate movement direction
        //    moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //    // on ground
        //    if (grounded)
        //        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //    // in air
        //    else if (!grounded)
        //        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        //}
        //else
        {
            var dir = transform.forward * moveSpeed * 10f;
            if (grounded)
            {
                rb.AddForce(dir, ForceMode.Force);
            }

            // in air
            else if (!grounded)
            {

                rb.AddForce(dir * airMultiplier, ForceMode.Force);
            }
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
       if (context.performed)
       {
            _jumpButton = true;
       }
       else if (context.canceled)
       {
            _jumpButton = false;
        }
    }
}
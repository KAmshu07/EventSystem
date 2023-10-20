using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    //public Animator animator;
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float slideSpeed = 15f;
    public float jumpForce = 5f;
    public float wallRunForce = 10f;
    public float wallRunDuration = 2f;
    public float wallRunCooldownDuration = 5f;
    public float airControlFactor = 0.5f;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isSprinting;
    private bool isSliding;
    private bool isWallRunning;
    private float wallRunTimer;
    private bool wallRunCooldownActive;
    private float wallRunCooldown;
    private float originalStepOffset;
    private Vector3 originalCenter;
    private Vector3 lastGroundedPosition;
    private Camera mainCamera;
    private float lastGroundedTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
        originalCenter = controller.center;
        mainCamera = Camera.main;
    }

    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Sprinting
        isSprinting = Input.GetButton("Sprint");

        // Sliding
        if (Input.GetButtonDown("Slide") && !isSliding && controller.isGrounded)
        {
            Slide();
        }

        // Wall Running
        if (!wallRunCooldownActive && !isWallRunning && controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump") && IsWallRunDetected())
            {
                StartWallRun();
            }
        }

        // Movement
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        if (!isSliding && !isWallRunning)
        {
            Vector3 targetMoveDirection = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f) * direction;
            if (controller.isGrounded)
            {
                moveDirection = Vector3.Lerp(moveDirection, targetMoveDirection, Time.deltaTime * 10f);
            }
            else
            {
                moveDirection = Vector3.Lerp(moveDirection, targetMoveDirection, Time.deltaTime * airControlFactor);
            }

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            // Calculate the rotation angle between the character's forward direction and the move direction
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;

            // Rotate the character towards the move direction
            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            // Update animator parameters
            UpdateAnimatorParameters();
        }

        // Jumping
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            lastGroundedPosition = transform.position;
        }
        else
        {
            ApplyGravity();
            CheckWallHang();
        }

        // Wall Run Timer
        if (isWallRunning)
        {
            wallRunTimer -= Time.deltaTime;
            if (wallRunTimer <= 0f)
            {
                StopWallRun();
            }
        }

        // Wall Run Cooldown
        if (wallRunCooldownActive)
        {
            wallRunCooldown -= Time.deltaTime;
            if (wallRunCooldown <= 0f)
            {
                wallRunCooldownActive = false;
            }
        }
    }

    private void UpdateAnimatorParameters()
    {
        // Calculate the horizontal and vertical values for the animator
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(horizontal, vertical);
        movement.Normalize();

        //// Set animator parameters based on the movement values
        //animator.SetFloat("SpeedRight", Mathf.Lerp(animator.GetFloat("SpeedRight"), movement.x, Time.deltaTime * 10f));
        //animator.SetFloat("SpeedForward", Mathf.Lerp(animator.GetFloat("SpeedForward"), movement.y, Time.deltaTime * 10f));

        //// Set additional animator parameters based on the character's actions
        //animator.SetBool("IsSprinting", isSprinting);
        //animator.SetBool("IsSliding", isSliding);
        //animator.SetBool("IsWallRunning", isWallRunning);
    }

    private void Slide()
    {
        Vector3 slideDirection = moveDirection;
        slideDirection.y = -slideSpeed;

        controller.Move(slideDirection * Time.deltaTime);
        isSliding = true;

        // Adjust character controller step offset and center for smooth sliding
        controller.stepOffset = 0f;
        controller.center = new Vector3(controller.center.x, originalCenter.y - (originalStepOffset - 0.1f), controller.center.z);

        // Restore step offset and center after sliding
        Invoke("ResetSlide", 0.5f);

        // Update animator parameters
        UpdateAnimatorParameters();
    }

    private void ResetSlide()
    {
        isSliding = false;
        controller.stepOffset = originalStepOffset;
        controller.center = originalCenter;

        // Update animator parameters
        UpdateAnimatorParameters();
    }

    private bool IsWallRunDetected()
    {
        // Perform raycasts to detect nearby walls and return true if a wall is detected.
        // You can customize this based on your level design and requirements.
        return false;
    }

    private void StartWallRun()
    {
        isWallRunning = true;
        wallRunTimer = wallRunDuration;

        // Apply wall run force and handle animations, particle effects, etc.

        // Update animator parameters
        UpdateAnimatorParameters();
    }

    private void StopWallRun()
    {
        isWallRunning = false;

        // Reset wall run variables and cooldown timer.
        wallRunCooldownActive = true;
        wallRunCooldown = wallRunCooldownDuration;

        // Handle animations, particle effects, etc.

        // Update animator parameters
        UpdateAnimatorParameters();
    }

    private void Jump()
    {
        if (isWallRunning)
        {
            moveDirection = Vector3.up * jumpForce;
            isWallRunning = false;
        }
        else
        {
            moveDirection.y = jumpForce;
        }

        // Update animator parameters
        UpdateAnimatorParameters();
    }

    private void ApplyGravity()
    {
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void CheckWallHang()
    {
        if (!isWallRunning && !controller.isGrounded)
        {
            float hangTime = 0.5f; // Adjust as needed
            if (Time.time - lastGroundedTime >= hangTime)
            {
                // Allow the player to grab onto a wall or perform other actions
                // while hanging in the air, such as wall jump or wall latch.
            }
        }
    }
}

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Serializable]
	public class MoveAxis
	{
		public KeyCode Positive;
		public KeyCode Negative;

		public MoveAxis(KeyCode positive, KeyCode negative)
		{
			Positive = positive;
			Negative = negative;
		}

		public float GetValue()
		{
			return (Input.GetKey(Positive) ? 1.0f : 0.0f) - (Input.GetKey(Negative) ? 1.0f : 0.0f);
		}
	}

	#region -------------------------Variables-------------------------

	[Header("Movement Speed")]
	[Tooltip("These are variables related to player's movement speed.")]
	public bool resetCurrentSpeed;
	public FloatVariable maxSpeed;
	public FloatVariable MoveRate;
	public FloatVariable acceleration;
	public FloatVariable currentSpeed;
	public FloatReference startingSpeed;

	[Header("Jump Settings")]
	[Tooltip("Controls the strength of the jump.")]
	public float jumpForce = 10.0f;

	[Space]
	[Header("Rotation Speed")]
	[Tooltip("Controls how fast the player rotates in the direction of camera's forward.")]
	public float rotationSpeed = 10.0f;

	[Space]
	[Header("Rigidbody")]
	[Tooltip("This is the player's Rigidbody.")]
	public Rigidbody rb;

	[Space]
	[Header("Keyboard Controls")]
	[Tooltip("These are the Keyboard controls of the player.")]
	public MoveAxis Horizontal = new MoveAxis(KeyCode.D, KeyCode.A);
	public MoveAxis Vertical = new MoveAxis(KeyCode.W, KeyCode.S);

	[Header("Camera")]
	private Transform playerCamera;

	[Header("Ground Check")]
	[Tooltip("Settings for ground detection using SphereCast.")]
	public Vector3 boxSize = new Vector3(0.9f,0.9f,0.9f);
	public float maxDistance = 0.1f;
	public LayerMask groundLayer;

	[Header("Gizmo Settings")]
	public Color groundedGizmoColor = Color.green;
	public Color notGroundedGizmoColor = Color.red;

	#endregion -------------------------Variables-------------------------

	#region ----------------------Unity Callbacks----------------------

	private void Start()
	{
		InitializeComponents();
	}

	private void Update()
	{
		HandleAllMovement();
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = GroundCheck() ? groundedGizmoColor : notGroundedGizmoColor;
		Vector3 boxCenter = transform.position - new Vector3(0f, boxSize.y * 0.5f, 0f);
		Gizmos.DrawWireCube(boxCenter, boxSize);
	}

	#endregion ---------------------Unity Callbacks---------------------

	#region -----------------------Initialization----------------------

	private void InitializeComponents()
	{
		rb = GetComponent<Rigidbody>();
		if (resetCurrentSpeed)
			currentSpeed.Value = startingSpeed.Value;

		playerCamera = Camera.main.transform;
	}

	#endregion -----------------------Initialization----------------------

	private void HandleAllMovement()
	{
		HandleMovement();
		HandleRotation();
		HandleJump();
	}

	private void HandleMovement()
	{
		Vector3 moveDirection = CalculateMoveDirection();
		float targetSpeed = moveDirection.magnitude * MoveRate.Value;

		currentSpeed.Value = Mathf.MoveTowards(currentSpeed.Value, targetSpeed, acceleration.Value * Time.deltaTime);
		currentSpeed.Value = Mathf.Clamp(currentSpeed.Value, 0, maxSpeed.Value);


		if (moveDirection != Vector3.zero) {
			var currentVelocity = moveDirection * currentSpeed.Value;
			rb.velocity = new Vector3(currentVelocity.x,rb.velocity.y,currentVelocity.z);

		}
	}

	private void HandleRotation()
	{
		Vector3 moveDirection = CalculateMoveDirection();

		if (moveDirection != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
		else
		{
			Quaternion targetRotation = Quaternion.LookRotation(transform.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
	}

	private Vector3 CalculateMoveDirection()
	{
		Vector3 cameraRight = playerCamera.right.normalized;
		Vector3 cameraForward = playerCamera.forward.normalized;
		cameraForward.y = 0;

		if (cameraForward != Vector3.zero)
		{
			return cameraForward * Vertical.GetValue() + cameraRight * Horizontal.GetValue();
		}

		return Vector3.zero;
	}

	private void HandleJump()
	{
		if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			Debug.Log("Player Jumped");
		}
	}

	private bool GroundCheck()
	{
		// Check if the player is grounded using a boxcast
		if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, groundLayer))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

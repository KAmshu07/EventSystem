using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float distance = 10.0f;
    public float height = 1.5f;
    public float smoothSpeed = 10.0f;
    public float rotationSpeed = 5.0f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float zoomSpeed = 2.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 20.0f;
    public LayerMask collisionLayers;
    public float collisionOffset = 0.2f;
    public float FOVOffset = 10.0f;

    private Transform target;
    private float yaw;
    private float pitch;
    private float currentDistance;
    private float desiredFOV;
    private float originalFOV;
    private Vector3 cameraOffset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentDistance = distance;
        desiredFOV = Camera.main.fieldOfView;
        originalFOV = desiredFOV;

        cameraOffset = new Vector3(0, height, 0);
    }

    private void Update()
    {
        if (target == null)
        {
            FindPlayer();
            return;
        }

        // Handle input in the Update method
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= zoomInput * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        // Calculate the desired position for the camera
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = new Vector3(0, 0, -currentDistance);
        Vector3 desiredPosition = target.position + target.TransformDirection(cameraOffset) + (rotation * offset);

        // Perform collision detection to avoid camera clipping into objects
        RaycastHit hitInfo;
        if (Physics.Linecast(target.TransformPoint(cameraOffset), desiredPosition, out hitInfo, collisionLayers))
        {
            desiredPosition = hitInfo.point + hitInfo.normal * collisionOffset;
        }

        // Apply camera smoothing using SmoothDamp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Update the camera position and rotation
        transform.position = smoothedPosition;
        transform.LookAt(target.position + target.TransformDirection(cameraOffset));

        // Adjust the field of view based on distance
        desiredFOV = originalFOV + FOVOffset * (currentDistance - distance) / (maxDistance - distance);
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, smoothSpeed * Time.deltaTime);
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("No object with the 'Player' tag found in the scene.");
        }
    }
}

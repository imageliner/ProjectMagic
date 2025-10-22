using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private CinemachineFollow camFollow;
    public float cameraZoomMax = 15;
    public float cameraZoomMin = 5;
    public float cameraZoomDefault = 12;
    private float targetZoom;
    public float zoomSpeed = 1;

    public Transform mousePosition;
    public Transform playerPosition;
    public float followStrength = 0.2f; // How much the camera pulls toward the mouse
    public float maxDistance = 5.0f; // Max distance from the player
    public float smoothSpeed = 5f; // Speed of interpolation

    public bool cameraLockToggle;


    void Start()
    {
        cameraLockToggle = true;

        targetZoom = camFollow.FollowOffset.y;
    }

    void Update()
    {
        Vector3 desiredPosition = Vector3.Lerp(playerPosition.position, mousePosition.position, followStrength);
        // Clamp the camera within a circle around the player
        Vector3 offset = desiredPosition - playerPosition.position;

        if (offset.magnitude > maxDistance)
        {
            offset = offset.normalized * maxDistance;
        }

        if (Input.GetKeyDown(KeyCode.C) && cameraLockToggle == true)
        {
            cameraLockToggle = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cameraLockToggle == false)
        {
            cameraLockToggle = true;
        }

        Vector3 targetPosition = cameraLockToggle
        ? playerPosition.position
        : playerPosition.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);


        if (Input.GetAxis("Mouse ScrollWheel") > 0f && targetZoom >= cameraZoomMin) // forward
        {
            targetZoom--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && targetZoom <= cameraZoomMax) // backwards
        {
            targetZoom++;
        }

        camFollow.FollowOffset.y = Mathf.Lerp(camFollow.FollowOffset.y, targetZoom, Time.deltaTime * zoomSpeed);
    }
}

using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private CinemachineFollow camFollow;
    public float cameraZoomMax = 15;
    public float cameraZoomMin = 2;
    public float cameraZoomDefault = 12;
    private float targetZoom;
    public float zoomSpeed = 1;

    public Transform mousePosition;
    public Transform playerPosition;
    public float followStrength = 0.2f;
    public float maxDistance = 5.0f;
    public float smoothSpeed = 5f;

    public bool cameraLockToggle;

    private float shakeTimer;
    private float originalFOV;

    private void Awake()
    {
        originalFOV = _camera.Lens.FieldOfView;
    }

    void Start()
    {
        cameraLockToggle = true;

        targetZoom = camFollow.FollowOffset.y;

        GameManager.singleton.hitstopManager.HitStop += CameraHitStop;
    }

    void Update()
    {
        Vector3 desiredPosition = Vector3.Lerp(playerPosition.position, mousePosition.position, followStrength);
        // clamp camera around player
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
            //cinemachine hard lookat z increase maybe
        }

        camFollow.FollowOffset.y = Mathf.Lerp(camFollow.FollowOffset.y, targetZoom, Time.deltaTime * zoomSpeed);
    }

    private void LateUpdate()
    {
        if (shakeTimer > 0)
        {
            if (_camera.Lens.FieldOfView > 55)
            {
                _camera.Lens.FieldOfView -= Time.deltaTime * 80;
            }
            
            shakeTimer -= Time.deltaTime;
        }else
        {
            _camera.Lens.FieldOfView = Mathf.Lerp(_camera.Lens.FieldOfView, 60, Time.deltaTime * 6);
        }
    }

    private void CameraHitStop()
    {
        originalFOV = _camera.Lens.FieldOfView;
        shakeTimer = 0.1f;
    }
}

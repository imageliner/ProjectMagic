using Unity.Cinemachine;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public Vector3 mouseWorldPosition;
    public Vector2 mouseScreenPosition;

    public Vector3 mouseAim;

    public void UpdateMousePosition(Vector2 screenPosition)
    {
        mouseScreenPosition = screenPosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            mouseWorldPosition = hit.point;
            transform.position = hit.point;
        }
    }
}

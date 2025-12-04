using UnityEngine;

public class UISpriteRotation : MonoBehaviour
{
    [SerializeField] private RectTransform imageToRotate;
    [SerializeField] private float rotationSpeed = 90f;

    private void Update()
    {
        imageToRotate.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}

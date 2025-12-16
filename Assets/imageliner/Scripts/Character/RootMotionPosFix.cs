using UnityEngine;

public class RootMotionPosFix : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
}

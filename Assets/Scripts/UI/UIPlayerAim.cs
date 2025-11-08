using UnityEngine;

public class UIPlayerAim : MonoBehaviour
{
    private Transform playerPos;

    private MouseTracker mouseTracker;


    private void Awake()
    {
        playerPos = FindAnyObjectByType<PlayerCharacter>().transform;
        mouseTracker = FindAnyObjectByType<MouseTracker>();
    }

    private void LateUpdate()
    {
        if (playerPos != null)
        {
            float posX = playerPos.position.x;
            float posY = playerPos.position.y + 0.2f;
            float posZ = playerPos.position.z;
            Vector3 aimPos = new Vector3(posX, posY , posZ);
            transform.position = aimPos;



            Vector3 mouseAngle = (mouseTracker.mouseWorldPosition - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(mouseAngle);
            lookRotation.x = 0;
            lookRotation.z = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 360f);

            mouseTracker.mouseAim = mouseAngle;



        }
    }
}

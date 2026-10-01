using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            // หัน UI เข้าหากล้องตลอดเวลา
            transform.rotation = mainCameraTransform.rotation;
        }
    }
}
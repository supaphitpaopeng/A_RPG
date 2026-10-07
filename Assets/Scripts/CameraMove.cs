using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform playerTransform;

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -6f);

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // คำนวณตำแหน่งที่กล้องควรจะไปตามตัวละคร
        Vector3 desiredPosition = playerTransform.position + offset;

        // เคลื่อนที่ตามอย่างนุ่มนวลเพื่อป้องกันอาการกระตุก (Jitter)
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
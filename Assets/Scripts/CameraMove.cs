using UnityEngine;
using UnityEngine.InputSystem; // 1. เรียกใช้งาน Namespace ของ New Input System

public class CameraMove : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform playerTransform;

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -6f);

    private float currentAngle = 0f;

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // 2. ใช้ New Input System ตรวจสอบการกดปุ่ม A / D หรือ ลูกศรซ้าย / ขวา
        float inputX = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                inputX -= 1f; // กดซ้าย (A) ได้ -1
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                inputX += 1f; // กดขวา (D) ได้ 1
        }

        // คำนวณการหมุนตามค่า Input ที่ได้
        currentAngle += inputX * rotationSpeed * Time.deltaTime;

        // 3. คำนวณตำแหน่งรอบตัวละคร (Orbit)
        float distanceXZ = Mathf.Sqrt(offset.x * offset.x + offset.z * offset.z);
        float rad = currentAngle * Mathf.Deg2Rad;

        float targetX = playerTransform.position.x + Mathf.Sin(rad) * distanceXZ;
        float targetZ = playerTransform.position.z + Mathf.Cos(rad) * distanceXZ;
        float targetY = playerTransform.position.y + offset.y;

        Vector3 desiredPosition = new Vector3(targetX, targetY, targetZ);

        // 4. เคลื่อนที่ตามอย่างนุ่มนวล
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 5. ให้กล้องหันหน้าไปมองที่ตัวละครเสมอ
        transform.LookAt(playerTransform.position + Vector3.up * (offset.y * 0.5f));
    }
}
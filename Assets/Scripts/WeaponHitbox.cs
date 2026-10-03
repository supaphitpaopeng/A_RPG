using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    [HideInInspector] public float currentDamage; // ดาเมจที่จะส่งไปทำร้ายศัตรู
    private bool canDealDamage = false;            // เปิด-ปิด การสร้างดาเมจ (เพื่อให้ทำดาเมจเฉพาะตอนฟัน)

    // เปิดการสร้างดาเมจ (จะเรียกผ่าน Animation Event ตอนจังหวะดาบเหวี่ยงโดน)
    public void EnableHitbox()
    {
        canDealDamage = true;
    }

    public void SetDamage(float damage)
    {
        currentDamage = damage;
    }

    // ปิดการสร้างดาเมจ (เรียกผ่าน Animation Event เมื่อสิ้นสุดท่าฟัน)
    public void DisableHitbox()
    {
        canDealDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        // เช็คว่าชนกับศัตรูหรือไม่ (โดยเช็คจาก EnemyHealth หรือ Tag "Enemy")
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(currentDamage);

            // ป้องกันการตีโดนซ้ำรัวๆ ในการโจมตีครั้งเดียว (ถ้าต้องการ)
            canDealDamage = false;
        }
    }
}
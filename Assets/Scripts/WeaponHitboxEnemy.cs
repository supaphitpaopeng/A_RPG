using UnityEngine;

public class WeaponHitboxEnemy : MonoBehaviour
{
    private Collider weaponCollider;
    [HideInInspector] public float currentDamage = 10f; // กำหนดดาเมจเริ่มต้นของศัตรู
    public bool canDealDamage = false;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        else
        {
            Debug.LogError("WeaponHitboxEnemy: No Collider found on the enemy.");
        }
    }

    // เปิดการสร้างดาเมจ (เรียกผ่าน Animation Event ตอนศัตรูออกท่าโจมตี)
    public void EnableHitbox()
    {
        canDealDamage = true;
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
    }

    public void SetDamage(float damage)
    {
        currentDamage = damage;
        Debug.Log("Enemy weapon damage set to: " + currentDamage);
    }

    // ปิดการสร้างดาเมจ (เรียกผ่าน Animation Event เมื่อจบการโจมตี)
    public void DisableHitbox()
    {
        canDealDamage = false;
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("WeaponHitboxEnemy: OnTriggerEnter called with " + other.name);
        if (!canDealDamage) return;

        // เช็คว่าชนโดนผู้เล่นหรือไม่ (โดยเช็คจากสคริปต์ CharacterStatus หรือ Tag "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit player with damage: " + currentDamage);
            CharacterStatus player = other.GetComponent<CharacterStatus>();
            
            
                player.TakeDamage(20);
            

            // ป้องกันการตีโดนซ้ำรัวๆ ในฮิตเดียว
            canDealDamage = false;
        }
    }
}
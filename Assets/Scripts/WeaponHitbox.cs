using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private Collider weaponCollider;
    [HideInInspector] public float currentDamage; // ดาเมจที่จะส่งไปทำร้ายศัตรู
    public float CurrentDamage => currentDamage; // เพิ่ม property เพื่อให้สามารถเข้าถึง currentDamage จากภายนอกได้
    public bool canDealDamage = false;            // เปิด-ปิด การสร้างดาเมจ (เพื่อให้ทำดาเมจเฉพาะตอนฟัน)
    public bool CanDealDamage => canDealDamage; // เพิ่ม property เพื่อให้สามารถเข้าถึง canDealDamage จากภายนอกได้
    [Header("Link all weapon hitboxes together")]
    [SerializeField] private WeaponHitbox[] allWeaponHitboxes;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false; 
        }
        else
        {
            Debug.LogError("WeaponHitbox: No Collider found on the weapon.");
        }
    }

    // เปิดการสร้างดาเมจ (จะเรียกผ่าน Animation Event ตอนจังหวะดาบเหวี่ยงโดน)
    public void EnableHitbox()
    {
        canDealDamage = true;
        if (weaponCollider != null)
        {
            weaponCollider.enabled = true;
        }
        if (allWeaponHitboxes != null)
        {
            foreach (var hitbox in allWeaponHitboxes)
            {
                if (hitbox != this) // ป้องกันการเรียกตัวเอง
                {
                    hitbox.canDealDamage = true;
                    if (hitbox.weaponCollider != null)
                    {
                        hitbox.weaponCollider.enabled = true;
                    }
                }
            }
        }
    }

    public void SetDamage(float damage)
    {
        currentDamage = damage;
        Debug.Log("Weapon damage set to: " + currentDamage);
        if (allWeaponHitboxes != null)
        {
            foreach (var hitbox in allWeaponHitboxes)
            {
                if (hitbox != this) // ป้องกันการเรียกตัวเอง
                {
                    hitbox.currentDamage = damage;
                    Debug.Log("Linked weapon damage set to: " + hitbox.currentDamage);
                }
            }
        }
    }

    // ปิดการสร้างดาเมจ (เรียกผ่าน Animation Event เมื่อสิ้นสุดท่าฟัน)
    public void DisableHitbox()
    {
        canDealDamage = false;
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }
        if (allWeaponHitboxes != null)
        {
            foreach (var hitbox in allWeaponHitboxes)
            {
                if (hitbox != this) // ป้องกันการเรียกตัวเอง
                {
                    hitbox.canDealDamage = false;
                    if (hitbox.weaponCollider != null)
                    {
                        hitbox.weaponCollider.enabled = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage) return;

        // เช็คว่าชนกับศัตรูหรือไม่ (โดยเช็คจาก EnemyHealth หรือ Tag "Enemy")
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            Debug.Log("Hit enemy: " + other.name + " with damage: " + currentDamage);
            Debug.Log("Weapon hit: " );
            enemy.TakeDamage(currentDamage);

            // ป้องกันการตีโดนซ้ำรัวๆ ในการโจมตีครั้งเดียว (ถ้าต้องการ)
            canDealDamage = false;
        }
    }

    private void Update()
    {
        //Debug.Log("Current Damage: " + currentDamage);
        //Debug.Log("Can Deal Damage: " + canDealDamage);
    }
}
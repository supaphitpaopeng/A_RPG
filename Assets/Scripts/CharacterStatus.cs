using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Character Status")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    public float CurrentHealth => currentHealth;
    public float CurrentMana => currentMana;
    public float MaxHealth => maxHealth;
    public float MaxMana => maxMana;

    [Header("Sound Reference")]
    [SerializeField] private PlayerSoundManager soundManager; // เชื่อมโยงกับ PlayerSoundManager

    Animator animator;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        animator = GetComponent<Animator>();

        // ถ้าไม่ได้ลากใส่ใน Inspector ให้ดึงจากตัวละครอัตโนมัติ
        if (soundManager == null)
        {
            soundManager = GetComponent<PlayerSoundManager>();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("Health is full.");
        }

        // สั่งเล่นเสียงกินยาเพิ่มเลือด
        if (soundManager != null)
        {
            soundManager.PlayHealthPotion();
        }

        //Debug.Log($"Healed {amount} points. Current Health: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Enemy took damage:");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void UseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana < 0)
        {
            currentMana = 0;
            //Debug.Log("Not enough mana.");
        }
        else
        {
            //Debug.Log($"Used {amount} mana. Current Mana: {currentMana}/{maxMana}");
        }
    }

    public void RestoreMana(float amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
            //Debug.Log("Mana is full.");
        }

        // สั่งเล่นเสียงกินยาเพิ่มมานา
        if (soundManager != null)
        {
            soundManager.PlayManaPotion();
        }

        //Debug.Log($"Restored {amount} mana. Current Mana: {currentMana}/{maxMana}");
    }

    public void Die()
    {
        if (isDead) return; // ป้องกันการเรียกฟังก์ชันตายซ้ำซ้อน
        isDead = true;

        Debug.Log("Character has died.");

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        Destroy(gameObject, 3f);
    }
}
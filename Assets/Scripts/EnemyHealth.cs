using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    Inventory inventory;
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthBarFill; // ลาก Image ตัว Fill มาใส่
    private float potionHealDropChance = 30f; // โอกาสในการดรอป Potion Heal (0-100)
    private float potionManaDropChance = 30f; // โอกาสในการดรอป Potion Mana (0-100)



    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // เรียกฟังก์ชันนี้เวลาโดนโจมตี
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log("Enemy took damage:");
        if (currentHealth <= 0)
        {
            Die();
            Drop();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Destroy(gameObject); // ลบตัวมอนสเตอร์ (หลอดเลือดจะถูกลบไปด้วย)
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            TakeDamage(20f); // ตัวอย่าง: โดนโจมตีลดเลือด 20 หน่วย
        }
    }*/
    private void Drop()
    {
        float randomValue = Random.Range(0f, 100f);
        Debug.Log("Random Value: " + randomValue);

        if (randomValue < potionManaDropChance)
        {
            inventory.AddPotionMana();
        }
        else if (randomValue < potionManaDropChance + potionHealDropChance)
        {
            inventory.AddPotionHeal();
        }
    }
}
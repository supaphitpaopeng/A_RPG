using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthBarFill; // ลาก Image ตัว Fill มาใส่

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
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
}
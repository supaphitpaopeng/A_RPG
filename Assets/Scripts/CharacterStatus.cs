using Unity.AI.Assistant.Agents;
using UnityEngine;
using UnityEngine.AI;

public class CharacterStatus : MonoBehaviour
{
    [Header("Character Status")]
    [SerializeField] private float maxHealth=100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxMana=100f;
    [SerializeField] private float currentMana;
    public float CurrentHealth => currentHealth;
    public float CurrentMana => currentMana;
    Animator animator;
    private bool isDead = false;

    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        animator = GetComponent<Animator>();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("Health is full.");
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
        else
        {
            //Debug.Log($"Restored {amount} mana. Current Mana: {currentMana}/{maxMana}");
        }
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
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(5f); // ตัวอย่าง: โดนโจมตีลดเลือด 5 หน่วย
        }
    }*/
}

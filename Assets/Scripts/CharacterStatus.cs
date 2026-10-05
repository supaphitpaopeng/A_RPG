using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Character Status")]
    [SerializeField] private float maxHealth=100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxMana=100f;
    [SerializeField] private float currentMana;
    public float CurrentHealth => currentHealth;
    public float CurrentMana => currentMana;

    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("Character is dead.");
            // You can add death logic here, like triggering an animation or disabling the character.
        }
        else
        {
            //Debug.Log($"Took {amount} damage. Current Health: {currentHealth}/{maxHealth}");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1f); // Take 10 damage from the enemy
            //Destroy(other.gameObject); // Remove the enemy from the scene
        }
        else if (other.CompareTag("HealthPotion"))
        {
            Heal(20f); // Heal 20 health points
            Destroy(other.gameObject); // Remove the potion from the scene
        }
        else if (other.CompareTag("ManaPotion"))
        {
            UseMana(-20f); // Restore 20 mana points
            Destroy(other.gameObject); // Remove the potion from the scene
        }
    }
}

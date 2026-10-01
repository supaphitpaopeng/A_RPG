using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [Header("Character Status")]
    [SerializeField] private float maxHealth=100f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxMana=100f;
    private float currentMana;
    public float CurrentHealth => currentHealth;

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
        Debug.Log($"Healed {amount} points. Current Health: {currentHealth}/{maxHealth}");
    }
}

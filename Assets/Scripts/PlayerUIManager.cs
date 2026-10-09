using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterStatus characterStatus;

    [Header("UI Bars")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image manaBarImage;

    private void Start()
    {
        if (characterStatus == null)
        {
            characterStatus = FindObjectOfType<CharacterStatus>();
        }
    }

    private void Update()
    {
        if (characterStatus == null) return;

        // อัปเดต fillAmount ของหลอดเลือด (0.0 ถึง 1.0)
        if (healthBarImage != null && characterStatus.MaxHealth > 0)
        {
            healthBarImage.fillAmount = characterStatus.CurrentHealth / characterStatus.MaxHealth;
        }

        // อัปเดต fillAmount ของหลอดมานา (0.0 ถึง 1.0)
        if (manaBarImage != null && characterStatus.MaxMana > 0)
        {
            manaBarImage.fillAmount = characterStatus.CurrentMana / characterStatus.MaxMana;
        }
    }
}
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private float currentpotionHeal = 2f;
    [SerializeField] private float currentpotionMana = 2f;
    public float CurrentPotionHeal => currentpotionHeal; // เพิ่ม property เพื่อให้สามารถเข้าถึง currentpotionHeal จากภายนอกได้
    public float CurrentPotionMana => currentpotionMana; // เพิ่ม property เพื่อให้สามารถเข้าถึง currentpotionMana จากภายนอกได้

    public void AddPotionHeal()
    {
        currentpotionHeal += 1f;
        Debug.Log("Current potion heal amount: " + currentpotionHeal);
    }
    public void UsePotionHeal()
    {
        if (currentpotionHeal > 0)
        {
            currentpotionHeal -= 1f;
            Debug.Log("Used a potion heal. Remaining potion heal amount: " + currentpotionHeal);
        }
    }
    public void AddPotionMana()
    {
        currentpotionMana += 1f;
        Debug.Log("Current potion mana amount: " + currentpotionMana);
    }
    public void UsePotionMana()
    {
        if (currentpotionMana > 0)
        {
            currentpotionMana -= 1f;
            Debug.Log("Used a potion mana. Remaining potion mana amount: " + currentpotionMana);
        }
    }

}

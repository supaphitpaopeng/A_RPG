using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Data", menuName = "Skill System/Potion Data")]
public class PotionData : ScriptableObject
{
    public string potionName;
    public float healAmount;      // จำนวนเลือดที่ฟื้นฟู
    public float manaAmount;      // จำนวนมานาที่ฟื้นฟู
    public float coolDown;        // เพิ่มเวลาคูลดาวน์ของยา (วินาที)
}
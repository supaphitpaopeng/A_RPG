using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill System/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public float manaCost;      // มานาที่ใช้
    public float baseDamage;    // ดาเมจพื้นฐาน
    public float coolDown;      // คูลดาวน์ (ถ้ามี)
}
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill System/Skill Data")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;          // ไอคอนหลัก (หรือไอคอนสำหรับ Twin Blade)
    public Sprite secondarySkillIcon; // <-- เพิ่มช่องไอคอนสำรอง (สำหรับ Dual Swords)
    public float manaCost;
    public float baseDamage;
    public float coolDown;
    public GameObject fxPrefab;
    public GameObject fxPrefab01;
    public GameObject fxPrefab02;
    public GameObject fxPrefab03;
}
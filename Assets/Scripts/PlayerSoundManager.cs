using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("--- Consumables (เสียงไอเทม) ---")]
    [SerializeField] private AudioClip healthPotionSound; // เสียงตอนกินยาเลือด
    [SerializeField] private AudioClip manaPotionSound;   // เสียงตอนกินยามานา

    [Header("--- TwinBlade Stance (ร่างเบลด) ---")]
    [SerializeField] private AudioClip twinBladeAttackSound;   // เสียงตีธรรมดาร่างเบลด
    [SerializeField] private AudioClip[] twinBladeSkillSounds; // เสียงสกิลร่างเบลด (E, Z, X, C ตามลำดับ)

    [Header("--- DualSwords Stance (ร่างดาบคู่) ---")]
    [SerializeField] private AudioClip dualSwordsAttackSound;   // เสียงตีธรรมดาร่างดาบคู่
    [SerializeField] private AudioClip[] dualSwordsSkillSounds; // เสียงสกิลร่างดาบคู่ (E, Z, X, C ตามลำดับ)

    [Header("--- Stance Change (เสียงกด Q เปลี่ยนร่าง) ---")]
    [SerializeField] private AudioClip switchStanceSound; // เสียงตอนกดเปลี่ยนร่าง

    // ตัวแปรเช็คร่างปัจจุบัน (true = ร่างดาบคู่, false = ร่างเบลด)
    private bool isDualSwordStance = false;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // 1. ฟังก์ชันเรียกเสียงกินยาเลือด
    public void PlayHealthPotion()
    {
        if (audioSource != null && healthPotionSound != null)
        {
            audioSource.PlayOneShot(healthPotionSound);
        }
    }

    // 2. ฟังก์ชันเรียกเสียงกินยามานา
    public void PlayManaPotion()
    {
        if (audioSource != null && manaPotionSound != null)
        {
            audioSource.PlayOneShot(manaPotionSound);
        }
    }

    // 3. ฟังก์ชันเรียกเสียงตีธรรมดา (แยกตามร่างปัจจุบันอัตโนมัติ)
    public void PlayNormalAttack(bool isDualSwords)
    {
        if (audioSource == null) return;

        if (isDualSwords && dualSwordsAttackSound != null)
        {
            audioSource.PlayOneShot(dualSwordsAttackSound);
        }
        else if (!isDualSwords && twinBladeAttackSound != null)
        {
            audioSource.PlayOneShot(twinBladeAttackSound);
        }
    }

    // 4. ฟังก์ชันเรียกเสียงสกิล (แยกตามร่างและลำดับปุ่ม E, Z, X, C)
    public void PlaySkillSound(bool isDualSwords, int skillIndex)
    {
        if (audioSource == null) return;

        if (isDualSwords)
        {
            if (dualSwordsSkillSounds != null && skillIndex < dualSwordsSkillSounds.Length && dualSwordsSkillSounds[skillIndex] != null)
            {
                audioSource.PlayOneShot(dualSwordsSkillSounds[skillIndex]);
            }
        }
        else
        {
            if (twinBladeSkillSounds != null && skillIndex < twinBladeSkillSounds.Length && twinBladeSkillSounds[skillIndex] != null)
            {
                audioSource.PlayOneShot(twinBladeSkillSounds[skillIndex]);
            }
        }
    }

    // 5. ฟังก์ชันกดปุ่ม Q เพื่อเปลี่ยนร่าง (สลับสถานะ และเล่นเสียงเปลี่ยนร่าง)
    public void ToggleStance(bool isDualSwordsActive)
    {
        isDualSwordStance = isDualSwordsActive;

        // เล่นเสียงเปลี่ยนร่าง
        if (audioSource != null && switchStanceSound != null)
        {
            audioSource.PlayOneShot(switchStanceSound);
        }

        Debug.Log(isDualSwordStance ? "Switched to Dual Swords Stance" : "Switched to TwinBlade Stance");
    }
}
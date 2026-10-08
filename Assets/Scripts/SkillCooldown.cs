using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldown : MonoBehaviour
{
    public enum SkillSlotType
    {
        Normal,
        SkillE,
        SkillZ,
        SkillX,
        SkillC,
        Transform,
        PotionHeal,
        PotionMana
    }

    [Header("Slot Type")]
    public SkillSlotType slotType;

    [Header("UI Components")]
    public Image cooldownImage;
    public Text cooldownText;
    public TMP_Text cooldownTMP;

    [Header("Key Display")]
    [Tooltip("ข้อความคีย์ลัดที่จะโชว์เมื่อสกิลพร้อมใช้ เช่น Space, E, Z, X, C, Q, 1, 2")]
    public string keyText = "E";

    private float currentCooldownTimer = 0f;
    private float maxCooldown = 0f;
    private bool isCooldown = false;

    void Awake()
    {
        if (cooldownImage == null)
        {
            Image[] images = GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                if (img.gameObject != this.gameObject)
                {
                    cooldownImage = img;
                    break;
                }
            }
        }

        if (cooldownText == null) cooldownText = GetComponentInChildren<Text>();
        if (cooldownTMP == null) cooldownTMP = GetComponentInChildren<TMP_Text>();
    }

    void OnEnable()
    {
        RegisterWithPlayer();
    }

    void Start()
    {
        if (cooldownImage != null) cooldownImage.fillAmount = 0f;
        ShowKeyText();
        RegisterWithPlayer();
    }

    public void RegisterWithPlayer()
    {
        Skill playerSkill = FindObjectOfType<Skill>();
        if (playerSkill != null)
        {
            playerSkill.RegisterUICooldown(slotType, this);
        }
    }

    void Update()
    {
        if (isCooldown)
        {
            currentCooldownTimer -= Time.deltaTime;

            if (cooldownImage != null && maxCooldown > 0)
            {
                cooldownImage.fillAmount = currentCooldownTimer / maxCooldown;
            }

            UpdateText(Mathf.Max(0, currentCooldownTimer).ToString("F1"));

            if (currentCooldownTimer <= 0f)
            {
                isCooldown = false;
                currentCooldownTimer = 0f;

                if (cooldownImage != null) cooldownImage.fillAmount = 0f;

                ShowKeyText();
            }
        }
    }

    public void StartCooldown(float duration)
    {
        if (duration <= 0f) return;

        maxCooldown = duration;
        currentCooldownTimer = duration;
        isCooldown = true;

        if (cooldownImage != null) cooldownImage.fillAmount = 1f;
        UpdateText(duration.ToString("F1"));
    }

    private void ShowKeyText()
    {
        UpdateText(keyText);
    }

    private void UpdateText(string text)
    {
        if (cooldownText != null) cooldownText.text = text;
        if (cooldownTMP != null) cooldownTMP.text = text;
    }

    public void SetSkillIcon(Sprite newIcon)
    {
        if (newIcon == null) return;

        // 1. เปลี่ยนรูปทุก Image ในวัตถุนี้และวัตถุลูก (ถ้าไม่ใช่ Cooldown Overlay)
        Image[] images = GetComponentsInChildren<Image>(true);
        foreach (Image img in images)
        {
            if (img != cooldownImage)
            {
                img.sprite = newIcon;
                img.color = Color.white; // รีเซ็ตสีไม่ให้ย้อมเป็นสีน้ำเงิน/ดำ
            }
        }

        // 2. เปลี่ยนรูปทุก RawImage ในวัตถุนี้และวัตถุลูก
        RawImage[] rawImages = GetComponentsInChildren<RawImage>(true);
        foreach (RawImage rawImg in rawImages)
        {
            rawImg.texture = newIcon.texture;
            rawImg.color = Color.white;
        }
    }
}
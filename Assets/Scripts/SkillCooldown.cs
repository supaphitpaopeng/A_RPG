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

    [Header("Disabled Indicator")]
    [Tooltip("ใส่ GameObject รูปกากบาทสีแดงที่นี่")]
    public GameObject disabledCrossImage;

    [Header("Key Display")]
    public string keyText = "E";

    private float currentCooldownTimer = 0f;
    private float maxCooldown = 0f;
    private bool isCooldown = false;
    private Inventory playerInventory;

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
        FindInventoryReference();
        ShowDefaultText();
        RegisterWithPlayer();
    }

    public void RegisterWithPlayer()
    {
        Skill playerSkill = FindObjectOfType<Skill>();
        if (playerSkill != null)
        {
            playerSkill.RegisterUICooldown(slotType, this);
            if (playerInventory == null)
            {
                playerInventory = playerSkill.GetComponent<Inventory>();
            }
        }
    }

    private void FindInventoryReference()
    {
        if (playerInventory == null)
        {
            Inventory inv = FindObjectOfType<Inventory>();
            if (inv != null) playerInventory = inv;
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

            // แสดงตัวเลขนับถอยหลังคูลดาวน์
            UpdateText(Mathf.CeilToInt(currentCooldownTimer).ToString());

            if (currentCooldownTimer <= 0f)
            {
                isCooldown = false;
                currentCooldownTimer = 0f;

                if (cooldownImage != null) cooldownImage.fillAmount = 0f;

                ShowDefaultText();
            }
        }
        else
        {
            // คอยอัปเดตจำนวนยาและกากบาทแบบ Realtime
            if (slotType == SkillSlotType.PotionHeal || slotType == SkillSlotType.PotionMana)
            {
                ShowDefaultText();
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

        // ขณะติดคูลดาวน์ ซ่อนกากบาทออกก่อนเพื่อแสดงเงาดำ/ตัวเลขคูลดาวน์
        if (disabledCrossImage != null) disabledCrossImage.SetActive(false);

        EnableTextObject(true);
        UpdateText(Mathf.CeilToInt(duration).ToString());
    }

    private void ShowDefaultText()
    {
        EnableTextObject(true);

        if (playerInventory != null)
        {
            float itemCount = 0f;
            bool isPotionSlot = false;

            if (slotType == SkillSlotType.PotionHeal)
            {
                itemCount = playerInventory.CurrentPotionHeal;
                isPotionSlot = true;
            }
            else if (slotType == SkillSlotType.PotionMana)
            {
                itemCount = playerInventory.CurrentPotionMana;
                isPotionSlot = true;
            }

            if (isPotionSlot)
            {
                // แปลงค่า float เป็น int ตอนแสดงผลข้อความเพื่อป้องกัน Error
                int displayAmount = Mathf.FloorToInt(itemCount);
                UpdateText(displayAmount.ToString());

                // แสดงกากบาทเมื่อไอเทมเหลือ 0 และไม่ได้ติดคูลดาวน์อยู่
                if (disabledCrossImage != null && !isCooldown)
                {
                    disabledCrossImage.SetActive(displayAmount <= 0);
                }
                return;
            }
        }

        if (disabledCrossImage != null) disabledCrossImage.SetActive(false);
        UpdateText(keyText);
    }

    private void UpdateText(string text)
    {
        if (cooldownText != null) cooldownText.text = text;
        if (cooldownTMP != null) cooldownTMP.text = text;
    }

    private void EnableTextObject(bool enable)
    {
        if (cooldownText != null) cooldownText.gameObject.SetActive(enable);
        if (cooldownTMP != null) cooldownTMP.gameObject.SetActive(enable);
    }

    public void SetSkillIcon(Sprite newIcon)
    {
        if (newIcon == null) return;

        Image[] images = GetComponentsInChildren<Image>(true);
        foreach (Image img in images)
        {
            if (img != cooldownImage && (disabledCrossImage == null || img.gameObject != disabledCrossImage))
            {
                img.sprite = newIcon;
                img.color = Color.white;
            }
        }

        RawImage[] rawImages = GetComponentsInChildren<RawImage>(true);
        foreach (RawImage rawImg in rawImages)
        {
            rawImg.texture = newIcon.texture;
            rawImg.color = Color.white;
        }
    }

    public void ResetCooldown()
    {
        isCooldown = false;
        currentCooldownTimer = 0f;

        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }

        ShowDefaultText();
    }
}
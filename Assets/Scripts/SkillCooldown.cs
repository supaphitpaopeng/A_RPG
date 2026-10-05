using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro; // รองรับ TextMeshPro

public class SkillCooldown : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Component")]
    public Image cooldownImage;
    [Tooltip("ใส่ Text หรือ TextMeshPro เพื่อแสดงตัวเลขนับถอยหลัง")]
    public Text cooldownText;
    public TMP_Text cooldownTMP;

    [Header("Cooldown Settings")]
    public float cooldownTime = 5f;

    [Header("Shortcut Key")]
    [Tooltip("พิมพ์ตัวอักษรที่ต้องการใช้กด เช่น q, w, e, r, space, f")]
    public string shortcutKey = "q";

    private float currentCooldownTimer = 0f;
    private bool isCooldown = false;

    void Awake()
    {
        // ค้นหา Image ตัวลูกให้อัตโนมัติถ้าไม่ได้ลากใส่
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

        // ค้นหา Text ตัวลูกให้อัตโนมัติ
        if (cooldownText == null) cooldownText = GetComponentInChildren<Text>();
        if (cooldownTMP == null) cooldownTMP = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }

        // ซ่อนข้อความคูลดาวน์ตอนเริ่มเกม
        UpdateCooldownText("");
    }

    void Update()
    {
        // ตรวจจับการกดคีย์ตามตัวอักษรที่พิมพ์ไว้
        if (!string.IsNullOrEmpty(shortcutKey))
        {
            try
            {
                if (Input.GetKeyDown(shortcutKey.ToLower()))
                {
                    UseSkill();
                }
            }
            catch
            {
                // ป้องกันกรณีพิมพ์ชื่อคีย์ผิด
            }
        }

        if (isCooldown)
        {
            currentCooldownTimer -= Time.deltaTime;

            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = currentCooldownTimer / cooldownTime;
            }

            // อัปเดตตัวเลขนับถอยหลัง (แสดงทศนิยม 1 ตำแหน่ง เช่น 4.2)
            UpdateCooldownText(Mathf.Max(0, currentCooldownTimer).ToString("F1"));

            if (currentCooldownTimer <= 0f)
            {
                isCooldown = false;
                currentCooldownTimer = 0f;

                if (cooldownImage != null)
                {
                    cooldownImage.fillAmount = 0f;
                }

                // เคลียร์ข้อความออกเมื่อคูลดาวน์เสร็จ
                UpdateCooldownText("");
            }
        }
    }

    private void UpdateCooldownText(string text)
    {
        if (cooldownText != null) cooldownText.text = text;
        if (cooldownTMP != null) cooldownTMP.text = text;
    }

    public void UseSkill()
    {
        if (isCooldown) return;

        Debug.Log(gameObject.name + " ใช้งานสกิล!");

        isCooldown = true;
        currentCooldownTimer = cooldownTime;

        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1f;
        }

        UpdateCooldownText(cooldownTime.ToString("F1"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UseSkill();
    }
}
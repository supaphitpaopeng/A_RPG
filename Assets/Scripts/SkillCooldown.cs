using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCooldown : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Component")]
    public Image cooldownImage;

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
    }

    void Start()
    {
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0f;
        }
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
                // ป้องกัน Crash กรณีพิมพ์ชื่อปุ่มผิด
            }
        }

        if (isCooldown)
        {
            currentCooldownTimer -= Time.deltaTime;

            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = currentCooldownTimer / cooldownTime;
            }

            if (currentCooldownTimer <= 0f)
            {
                isCooldown = false;
                currentCooldownTimer = 0f;

                if (cooldownImage != null)
                {
                    cooldownImage.fillAmount = 0f;
                }
            }
        }
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UseSkill();
    }
}
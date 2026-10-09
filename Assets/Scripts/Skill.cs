using UnityEngine;
using UnityEngine.InputSystem;

public class Skill : MonoBehaviour
{
    Animator animator;
    ClickToMove clickToMove;
    CharacterStatus characterStatus;
    EnemyHealth enemyHealth;
    WeaponHitbox weaponHitbox;
    Inventory inventory;
    private bool canAttack = true;
    public bool CanAttack => canAttack;
    private bool canPotion = true;
    public bool CanPotion => canPotion;
    private bool canTransform = true;

    [Header("Weapon GameObjects")]
    [SerializeField] private GameObject leftSwordsObject;
    [SerializeField] private GameObject rightSwordsObject;
    [SerializeField] private GameObject twinBladeObject;
    private bool isDualSwordsActive = false;
    private bool isTwinBladeActive = true;
    public bool IsDualSwordsActive => isDualSwordsActive;
    public bool IsTwinBladeActive => isTwinBladeActive;

    [Header("TwinBlade Skill Data")]
    [SerializeField] private SkillData twinBladeSkillEData;
    [SerializeField] private SkillData twinBladeSkillZData;
    [SerializeField] private SkillData twinBladeSkillXData;
    [SerializeField] private SkillData twinBladeSkillCData;
    [SerializeField] private SkillData twinBladeSkillNormalData;

    [Header("DualSwords Skill Data")]
    [SerializeField] private SkillData dualSwordsSkillEData;
    [SerializeField] private SkillData dualSwordsSkillZData;
    [SerializeField] private SkillData dualSwordsSkillXData;
    [SerializeField] private SkillData dualSwordsSkillCData;
    [SerializeField] private SkillData dualSwordsSkillNormalData;

    [Header("UI Cooldown References")]
    [SerializeField] private SkillCooldown normalSkillCooldownUI;
    [SerializeField] private SkillCooldown skillECooldownUI;
    [SerializeField] private SkillCooldown skillZCooldownUI;
    [SerializeField] private SkillCooldown skillXCooldownUI;
    [SerializeField] private SkillCooldown skillCCooldownUI;
    [SerializeField] private SkillCooldown transformCooldownUI;
    [SerializeField] private SkillCooldown potionHealCooldownUI;
    [SerializeField] private SkillCooldown potionManaCooldownUI;

    [SerializeField] private SkillData transformSkillData;

    [Header("Potion Data")]
    [SerializeField] private PotionData potionHealData;
    [SerializeField] private PotionData potionManaData;

    [Header("Weapon Hitbox Reference")]
    [SerializeField] private WeaponHitbox activeWeaponHitbox;
    private SkillData currentActiveSkillData;

    private float AttackTime = 0f;
    private float twinBladenextETime = 0f;
    private float twinBladeNextZTime = 0f;
    private float twinBladeNextXTime = 0f;
    private float twinBladeNextCTime = 0f;
    private float dualSwordsNextETime = 0f;
    private float dualSwordsNextZTime = 0f;
    private float dualSwordsNextXTime = 0f;
    private float dualSwordsNextCTime = 0f;
    private float TransformTime = 0f;

    // --- ตัวแปรล็อกคูลดาวน์ยา ---
    private float nextPotionHealTime = 0f;
    private float nextPotionManaTime = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clickToMove = GetComponent<ClickToMove>();
        characterStatus = GetComponent<CharacterStatus>();
        weaponHitbox = GetComponentInChildren<WeaponHitbox>();
        inventory = GetComponent<Inventory>();
        if (leftSwordsObject != null) leftSwordsObject.SetActive(false);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(false);
        if (twinBladeObject != null) twinBladeObject.SetActive(true);
    }

    void Update()
    {
        keydownskill();
    }

    public void RegisterUICooldown(SkillCooldown.SkillSlotType slotType, SkillCooldown uiSlot)
    {
        switch (slotType)
        {
            case SkillCooldown.SkillSlotType.Normal:
                normalSkillCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.SkillE:
                skillECooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.SkillZ:
                skillZCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.SkillX:
                skillXCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.SkillC:
                skillCCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.Transform:
                transformCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.PotionHeal:
                potionHealCooldownUI = uiSlot;
                break;
            case SkillCooldown.SkillSlotType.PotionMana:
                potionManaCooldownUI = uiSlot;
                break;
        }

        UpdateSkillIcons();
    }

    public void UpdateSkillIcons()
    {
        // ล้างคูลดาวน์เก่าของปุ่มสกิล E, Z, X, C, Normal เมื่อเปลี่ยนสาย
        if (skillECooldownUI != null) skillECooldownUI.ResetCooldown();
        if (skillZCooldownUI != null) skillZCooldownUI.ResetCooldown();
        if (skillXCooldownUI != null) skillXCooldownUI.ResetCooldown();
        if (skillCCooldownUI != null) skillCCooldownUI.ResetCooldown();
        if (normalSkillCooldownUI != null) normalSkillCooldownUI.ResetCooldown();

        if (isTwinBladeActive)
        {
            if (skillECooldownUI != null && twinBladeSkillEData != null) skillECooldownUI.SetSkillIcon(twinBladeSkillEData.skillIcon);
            if (skillZCooldownUI != null && twinBladeSkillZData != null) skillZCooldownUI.SetSkillIcon(twinBladeSkillZData.skillIcon);
            if (skillXCooldownUI != null && twinBladeSkillXData != null) skillXCooldownUI.SetSkillIcon(twinBladeSkillXData.skillIcon);
            if (skillCCooldownUI != null && twinBladeSkillCData != null) skillCCooldownUI.SetSkillIcon(twinBladeSkillCData.skillIcon);
            if (normalSkillCooldownUI != null && twinBladeSkillNormalData != null) normalSkillCooldownUI.SetSkillIcon(twinBladeSkillNormalData.skillIcon);

            if (transformCooldownUI != null && transformSkillData != null)
            {
                transformCooldownUI.SetSkillIcon(transformSkillData.skillIcon);
            }
        }
        else if (isDualSwordsActive)
        {
            if (skillECooldownUI != null && dualSwordsSkillEData != null) skillECooldownUI.SetSkillIcon(dualSwordsSkillEData.skillIcon);
            if (skillZCooldownUI != null && dualSwordsSkillZData != null) skillZCooldownUI.SetSkillIcon(dualSwordsSkillZData.skillIcon);
            if (skillXCooldownUI != null && dualSwordsSkillXData != null) skillXCooldownUI.SetSkillIcon(dualSwordsSkillXData.skillIcon);
            if (skillCCooldownUI != null && dualSwordsSkillCData != null) skillCCooldownUI.SetSkillIcon(dualSwordsSkillCData.skillIcon);
            if (normalSkillCooldownUI != null && dualSwordsSkillNormalData != null) normalSkillCooldownUI.SetSkillIcon(dualSwordsSkillNormalData.skillIcon);

            if (transformCooldownUI != null && transformSkillData != null)
            {
                transformCooldownUI.SetSkillIcon(transformSkillData.secondarySkillIcon);
            }
        }
    }

    private void keydownskill()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    attackTwinBlade();
                }
                else if (isDualSwordsActive)
                {
                    attackDualSwords();
                }
            }
        }

        // --- Potion Heal (ปุ่ม 1) ---
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            if (canPotion && canAttack)
            {
                if (Time.time < nextPotionHealTime)
                {
                    Debug.Log("Potion Heal is on cooldown!");
                    return;
                }

                if (inventory != null && inventory.CurrentPotionHeal > 0)
                {
                    UsePotion(potionHealData, ref nextPotionHealTime, potionHealCooldownUI);
                    inventory.UsePotionHeal();
                }
            }
        }

        // --- Potion Mana (ปุ่ม 2) ---
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            if (canPotion && canAttack)
            {
                if (Time.time < nextPotionManaTime)
                {
                    Debug.Log("Potion Mana is on cooldown!");
                    return;
                }

                if (inventory != null && inventory.CurrentPotionMana > 0)
                {
                    UsePotion(potionManaData, ref nextPotionManaTime, potionManaCooldownUI);
                    inventory.UsePotionMana();
                }
            }
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                Transform();
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive) TwinBladeSkillE();
                else if (isDualSwordsActive) DualSwordsSkillE();
            }
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive) TwinBladeSkillZ();
                else if (isDualSwordsActive) DualSwordsSkillZ();
            }
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive) TwinBladeSkillX();
                else if (isDualSwordsActive) DualSwordsSkillX();
            }
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive) TwinBladeSkillC();
                else if (isDualSwordsActive) DualSwordsSkillC();
            }
        }
    }

    private void attackTwinBlade()
    {
        ExecuteSkill(twinBladeSkillNormalData, "T_Attack1", ref AttackTime, normalSkillCooldownUI);
    }

    private void TwinBladeSkillE()
    {
        ExecuteSkill(twinBladeSkillEData, "T_Attack_E", ref twinBladenextETime, skillECooldownUI);
    }

    private void TwinBladeSkillZ()
    {
        ExecuteSkill(twinBladeSkillZData, "T_Attack_Z", ref twinBladeNextZTime, skillZCooldownUI);
    }

    private void TwinBladeSkillX()
    {
        ExecuteSkill(twinBladeSkillXData, "T_Attack_X", ref twinBladeNextXTime, skillXCooldownUI);
    }

    private void TwinBladeSkillC()
    {
        ExecuteSkill(twinBladeSkillCData, "T_Attack_C", ref twinBladeNextCTime, skillCCooldownUI);
    }

    private void attackDualSwords()
    {
        ExecuteSkill(dualSwordsSkillNormalData, "D_Attack1", ref AttackTime, normalSkillCooldownUI);
    }

    private void DualSwordsSkillE()
    {
        ExecuteSkill(dualSwordsSkillEData, "D_Attack_E", ref dualSwordsNextETime, skillECooldownUI);
    }

    private void DualSwordsSkillZ()
    {
        ExecuteSkill(dualSwordsSkillZData, "D_Attack_Z", ref dualSwordsNextZTime, skillZCooldownUI);
    }

    private void DualSwordsSkillX()
    {
        ExecuteSkill(dualSwordsSkillXData, "D_Attack_X", ref dualSwordsNextXTime, skillXCooldownUI);
    }

    private void DualSwordsSkillC()
    {
        ExecuteSkill(dualSwordsSkillCData, "D_Attack_C", ref dualSwordsNextCTime, skillCCooldownUI);
    }

    private void ExecuteSkill(SkillData skillData, string animTrigger, ref float nextSkillTime, SkillCooldown uiSlot = null)
    {
        if (skillData == null) return;

        if (Time.time < nextSkillTime)
        {
            Debug.Log("Skill is on cooldown!");
            return;
        }

        if (characterStatus != null)
        {
            if (characterStatus.CurrentMana < skillData.manaCost)
            {
                Debug.Log($"Not enough mana to use {skillData.skillName}.");
                return;
            }

            characterStatus.UseMana(skillData.manaCost);
            if (weaponHitbox != null) weaponHitbox.SetDamage(skillData.baseDamage);

            nextSkillTime = Time.time + skillData.coolDown;

            if (uiSlot != null)
            {
                uiSlot.StartCooldown(skillData.coolDown);
            }
        }

        currentActiveSkillData = skillData;
        canAttack = false;
        if (clickToMove != null) clickToMove.StopMovement();
        animator.SetTrigger(animTrigger);
    }

    private void UsePotion(PotionData potionData, ref float nextPotionTime, SkillCooldown uiSlot = null)
    {
        if (potionData == null) return;

        canPotion = false;
        if (clickToMove != null) clickToMove.StopMovement();

        characterStatus.Heal(potionData.healAmount);
        characterStatus.RestoreMana(potionData.manaAmount);
        animator.SetTrigger("Potion");

        float cdDuration = (potionData.coolDown > 0) ? potionData.coolDown : 2.0f;
        nextPotionTime = Time.time + cdDuration;

        if (uiSlot != null)
        {
            uiSlot.StartCooldown(cdDuration);
        }
    }

    private void Transform()
    {
        if (Time.time < TransformTime)
        {
            Debug.Log($"Transform is on cooldown. Time remaining: {TransformTime - Time.time:F2} seconds.");
            return;
        }

        if (transformSkillData != null && characterStatus.CurrentMana >= transformSkillData.manaCost)
        {
            characterStatus.UseMana(transformSkillData.manaCost);
            TransformTime = Time.time + transformSkillData.coolDown;

            if (transformCooldownUI != null)
            {
                transformCooldownUI.StartCooldown(transformSkillData.coolDown);
            }

            if (!isDualSwordsActive && isTwinBladeActive)
            {
                animator.SetBool("Transform", true);
                canTransform = false;
                if (clickToMove != null) clickToMove.StopMovement();
            }
            else
            {
                animator.SetBool("Transform", false);
                canTransform = true;
                if (clickToMove != null) clickToMove.StopMovement();
            }
        }
    }

    public void EquipTwinBlade()
    {
        if (leftSwordsObject != null) leftSwordsObject.SetActive(false);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(false);
        if (twinBladeObject != null) twinBladeObject.SetActive(true);
        isDualSwordsActive = false;
        isTwinBladeActive = true;

        UpdateSkillIcons();
    }

    public void EquipDualSwords()
    {
        if (leftSwordsObject != null) leftSwordsObject.SetActive(true);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(true);
        if (twinBladeObject != null) twinBladeObject.SetActive(false);
        isDualSwordsActive = true;
        isTwinBladeActive = false;

        UpdateSkillIcons();
    }

    public void ActivateCanAttack() => canAttack = false;
    public void ResetCanAttack() => canAttack = true;
    public void ActivateCanPotion() => canPotion = false;
    public void ResetCanPotion() => canPotion = true;

    public void ActivateHitbox() { if (weaponHitbox != null) weaponHitbox.EnableHitbox(); }
    public void DeactivateHitbox() { if (weaponHitbox != null) weaponHitbox.DisableHitbox(); }

    public void TriggerSkillEffectEvent()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab, spawnPos, transform.rotation);
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent1_1()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab01 != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab01, spawnPos, transform.rotation);
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent_1()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab != null)
        {
            Vector3 SpawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab, SpawnPos, transform.rotation);
            Vector3 currentScale = fx.transform.localScale;
            currentScale.x *= -1;
            fx.transform.localScale = currentScale;
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent2_2()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab02 != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab02, spawnPos, transform.rotation);
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent02()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab02 != null)
        {
            Vector3 SpawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab02, SpawnPos, transform.rotation);
            Vector3 currentScale = fx.transform.localScale;
            currentScale.x *= -1;
            fx.transform.localScale = currentScale;
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent02_2()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab03 != null)
        {
            Vector3 SpawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab03, SpawnPos, transform.rotation);
            Vector3 currentScale = fx.transform.localScale;
            currentScale.x *= -1;
            fx.transform.localScale = currentScale;
            Destroy(fx, 2f);
        }
    }

    public void TriggerSkillEffectEvent02_1()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab02 != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            Quaternion spawnRotation = transform.rotation * Quaternion.Euler(90, 0, 0);
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab02, spawnPos, spawnRotation);
            Destroy(fx, 2f);
        }
    }

    public void ActivateSword()
    {
        if (isTwinBladeActive)
        {
            if (twinBladeObject != null) twinBladeObject.SetActive(true);
            if (leftSwordsObject != null) leftSwordsObject.SetActive(false);
            if (rightSwordsObject != null) rightSwordsObject.SetActive(false);
        }
        else if (isDualSwordsActive)
        {
            if (leftSwordsObject != null) leftSwordsObject.SetActive(true);
            if (rightSwordsObject != null) rightSwordsObject.SetActive(true);
            if (twinBladeObject != null) twinBladeObject.SetActive(false);
        }
    }

    public void DeactivateSword()
    {
        if (twinBladeObject != null) twinBladeObject.SetActive(false);
        if (leftSwordsObject != null) leftSwordsObject.SetActive(false);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(false);
    }
}
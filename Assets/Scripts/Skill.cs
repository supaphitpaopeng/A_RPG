using UnityEngine;
using UnityEngine.InputSystem;

public class Skill : MonoBehaviour
{
    Animator animator;
    ClickToMove clickToMove;
    CharacterStatus characterStatus;
    EnemyHealth enemyHealth;
    WeaponHitbox weaponHitbox;
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

    [SerializeField] private SkillData transformSkillData;

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



    private void Awake()
    {
        animator = GetComponent<Animator>();
        clickToMove = GetComponent<ClickToMove>();
        characterStatus = GetComponent<CharacterStatus>();
        weaponHitbox = GetComponentInChildren<WeaponHitbox>();
    }


    // Update is called once per frame
    void Update()
    {
        keydownskill();
        
        //Debug.Log($"CanAttack: {canAttack}, CanPotion: {canPotion}");


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

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (canPotion && canAttack)
            {
                UsePotion();
            }
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log($"CanTransform: {canTransform}, IsDualSwordsActive: {isDualSwordsActive}, IsTwinBladeActive: {isTwinBladeActive}");
        }
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    Transform();
                }
                else if (isDualSwordsActive)
                {
                    Transform();
                }
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    TwinBladeSkillE();
                }
                else if (isDualSwordsActive)
                {
                    DualSwordsSkillE();
                }
            }
        }


        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    TwinBladeSkillZ();
                }
                else if (isDualSwordsActive)
                {
                    DualSwordsSkillZ();
                }
            }
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    TwinBladeSkillX();
                }
                else if (isDualSwordsActive)
                {
                    DualSwordsSkillX();
                }
            }
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            if (canAttack && canPotion)
            {
                if (isTwinBladeActive)
                {
                    TwinBladeSkillC();
                }
                else if (isDualSwordsActive)
                {
                    DualSwordsSkillC();
                }
            }
        }
    }

    private void attackTwinBlade()
    {
        
        ExecuteSkill(twinBladeSkillNormalData, "T_Attack1", ref AttackTime);
    }

    private void TwinBladeSkillE()
    {
        ExecuteSkill(twinBladeSkillEData, "T_Attack_E", ref twinBladenextETime);
    }
    private void TwinBladeSkillZ()
    {
        ExecuteSkill(twinBladeSkillZData, "T_Attack_Z", ref twinBladeNextZTime);
    }
    private void TwinBladeSkillX()
    {
        ExecuteSkill(twinBladeSkillXData, "T_Attack_X", ref twinBladeNextXTime);
    }
    private void TwinBladeSkillC()
    {
        ExecuteSkill(twinBladeSkillCData, "T_Attack_C", ref twinBladeNextCTime);
    }

    private void attackDualSwords()
    {
        ExecuteSkill(dualSwordsSkillNormalData, "D_Attack1", ref AttackTime);
    }
    private void DualSwordsSkillE()
    {
        ExecuteSkill(dualSwordsSkillEData, "D_Attack_E", ref dualSwordsNextETime);
    }
    private void DualSwordsSkillZ()
    {
        ExecuteSkill(dualSwordsSkillZData, "D_Attack_Z", ref dualSwordsNextZTime);
    }
    private void DualSwordsSkillX()
    {
        ExecuteSkill(dualSwordsSkillXData, "D_Attack_X", ref dualSwordsNextXTime);
    }
    private void DualSwordsSkillC()
    {
        ExecuteSkill(dualSwordsSkillCData, "D_Attack_C", ref dualSwordsNextCTime);
    }

    private void UsePotion()
    {
        canPotion = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        characterStatus.Heal(50f);
        animator.SetTrigger("Potion");
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
            if (!isDualSwordsActive && isTwinBladeActive)
            {
                animator.SetBool("Transform", true);
                canTransform = false;
                clickToMove.StopMovement();
                Debug.Log("Transforming to Dual Swords");
            }
            else
            {
                animator.SetBool("Transform", false);
                canTransform = true;
                clickToMove.StopMovement();
            }
        }
    }

    public void EquipTwinBlade()
    {
        Debug.Log("Equipping Twin Blade");
        if (leftSwordsObject != null) leftSwordsObject.SetActive(false);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(false);
        if (twinBladeObject != null) twinBladeObject.SetActive(true);
        isDualSwordsActive = false;
        isTwinBladeActive = true;
    }

    public void EquipDualSwords()
    {
        Debug.Log("Equipping Dual Swords");
        if (leftSwordsObject != null) leftSwordsObject.SetActive(true);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(true);
        if (twinBladeObject != null) twinBladeObject.SetActive(false);
        isDualSwordsActive = true;
        isTwinBladeActive = false;  
    }


    public void ActivateCanAttack()
    {
        canAttack = false;
    }
    public void ResetCanAttack()
    {
        canAttack = true;
    }
    public void ActivateCanPotion()
    {
        canPotion = false;
    }
    public void ResetCanPotion()
    {
        canPotion = true;
    }

    public void ActivateHitbox()
    {
        weaponHitbox.EnableHitbox();
    }
    public void DeactivateHitbox()
    {
        weaponHitbox.DisableHitbox();
    }

    private void ExecuteSkill(SkillData skillData, string animTrigger, ref float nextSkillTime)
    {
        if (skillData == null) return;

        // 1. เช็คคูลดาวน์
        if (Time.time < nextSkillTime)
        {
            Debug.Log("Skill is on cooldown!");
            return;
        }

        // 2. เช็คมานา
        if (characterStatus != null)
        {
            if (characterStatus.CurrentMana < skillData.manaCost)
            {
                Debug.Log($"Not enough mana to use {skillData.skillName}.");
                return;
            }

            characterStatus.UseMana(skillData.manaCost);
            weaponHitbox.SetDamage(skillData.baseDamage);

            // ตั้งค่าคูลดาวน์
            nextSkillTime = Time.time + skillData.coolDown;
        }

        // 3. บันทึกข้อมูลสกิลนี้ไว้ให้ Animation Event เรียกใช้เอฟเฟกต์ถูกจังหวะ
        currentActiveSkillData = skillData;

        // 4. ล็อคการเคลื่อนไหวและสั่งเล่นอนิเมชัน
        canAttack = false;
        if (clickToMove != null) clickToMove.StopMovement();
        animator.SetTrigger(animTrigger);
    }
    public void TriggerSkillEffectEvent()
    {
        if (currentActiveSkillData != null && currentActiveSkillData.fxPrefab != null)
        {
            // สร้างเอฟเฟกต์ตรงตำแหน่งตัวละคร (หรือปรับระยะหน้าตัวละครได้ด้วย transform.forward)
            Vector3 spawnPos = transform.position + transform.forward * 1.0f + Vector3.up;
            GameObject fx = Instantiate(currentActiveSkillData.fxPrefab, spawnPos, transform.rotation);
            Destroy(fx, 2f);

        }
    }
    
}

using UnityEngine;
using UnityEngine.InputSystem;

public class Skill : MonoBehaviour
{
    Animator animator;
    ClickToMove clickToMove;
    CharacterStatus characterStatus;
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clickToMove = GetComponent<ClickToMove>();
        characterStatus = GetComponent<CharacterStatus>();
    }


    // Update is called once per frame
    void Update()
    {
        keydownskill();
        CheckCanAttack();
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
                    Debug.Log("Dual Swords Attack");
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
                    Debug.Log("Dual Swords Skill E");
                }
            }
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
                    Debug.Log("Dual Swords Skill Z");
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
                    Debug.Log("Dual Swords Skill X");
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
                    Debug.Log("Dual Swords Skill C");
                }
            }
        }
    }

    private void attackTwinBlade()
    {
        canAttack = false; 
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        
        animator.SetTrigger("T_Attack1"); 
    }

    private void TwinBladeSkillE()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("T_Attack_E");
    }
    private void TwinBladeSkillQ()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("T_Attack_Q");
    }
    private void TwinBladeSkillZ()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("T_Attack_Z");
    }
    private void TwinBladeSkillX()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("T_Attack_X");
    }
    private void TwinBladeSkillC()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("T_Attack_C");
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
        
        if (!isDualSwordsActive && isTwinBladeActive)
        { 
            animator.SetBool("test", true);
            canTransform = false;
            clickToMove.StopMovement();
            canAttack = false;

            Debug.Log("Transforming to Dual Swords");
        }
        else
        { 
            animator.SetBool("test", false);
            canTransform = true;
            clickToMove.StopMovement();
            canAttack = false;
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
        canAttack = true;
    }

    public void EquipDualSwords()
    {
        Debug.Log("Equipping Dual Swords");
        if (leftSwordsObject != null) leftSwordsObject.SetActive(true);
        if (rightSwordsObject != null) rightSwordsObject.SetActive(true);
        if (twinBladeObject != null) twinBladeObject.SetActive(false);
        isDualSwordsActive = true;
        isTwinBladeActive = false;
        canAttack = true;
    }

    private void CheckCanAttack()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log($"Current State: {stateInfo.fullPathHash}, Normalized Time: {stateInfo.normalizedTime}");

        // เช็คว่าถ้าเข้าสู่ State "Attack" แล้ว และเล่นจบครบ 1 รอบ (normalizedTime >= 1.0f)
        if (stateInfo.IsName("Attack_1") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("Action_A_4_1") && stateInfo.normalizedTime >= 0.9f)
        {
            canPotion = true; // 
        }
        if (stateInfo.IsName("T_Attack_Q_Transform") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("D_Attack_Q_Transform") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("T_Attack_E") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("T_Attack_Q") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("T_Attack_Z") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("T_Attack_X") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("T_Attack_C") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }


    }

    
    
}

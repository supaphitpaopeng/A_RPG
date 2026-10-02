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
        
        Debug.Log($"CanAttack: {canAttack}, CanPotion: {canPotion}");


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

    private void attackDualSwords()
    {
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }

        animator.SetTrigger("D_Attack1");
    }
    private void DualSwordsSkillE()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("D_Attack_E");
    }
    private void DualSwordsSkillZ()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("D_Attack_Z");
    }
    private void DualSwordsSkillX()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("D_Attack_X");
    }
    private void DualSwordsSkillC()
    {
        canAttack = false;
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        animator.SetTrigger("D_Attack_C");
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

}

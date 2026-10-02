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
                attack();
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (canPotion && canAttack)
            {
                UsePotion();
            }
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            testanimator();
        }
    }

    private void attack()
    {
        canAttack = false; 
        if (clickToMove != null)
        {
            clickToMove.StopMovement();
        }
        
        animator.SetTrigger("Attack"); 
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

    private void testanimator()
    {
        canAttack = false;
        if (!isDualSwordsActive && isTwinBladeActive)
        { 
            animator.SetBool("test", true);
            canTransform = false;
           
            Debug.Log("Transforming to Dual Swords");
        }
        else
        { 
            animator.SetBool("test", false);
            canTransform = true;
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

    private void CheckCanAttack()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //Debug.Log($"Current State: {stateInfo.fullPathHash}, Normalized Time: {stateInfo.normalizedTime}");

        // เช็คว่าถ้าเข้าสู่ State "Attack" แล้ว และเล่นจบครบ 1 รอบ (normalizedTime >= 1.0f)
        if (stateInfo.IsName("1Hand_Up_Attack_A_1") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }
        if (stateInfo.IsName("Action_A_4_1") && stateInfo.normalizedTime >= 0.9f)
        {
            canPotion = true; // 
        }
        if (stateInfo.IsName("Blades2Sword_Transform_01_Root") && stateInfo.normalizedTime >= 0.9f)
        {
            canAttack = true; // 
        }


    }

    
    
}

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
    }
}

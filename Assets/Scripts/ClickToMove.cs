using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float stoppingDistanse = 0.2f;

    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private float Range = 5f;

    NavMeshAgent agent;
    Camera mainCamera;
    Animator animator;
    Skill skill;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        agent.stoppingDistance = stoppingDistanse;
        skill = GetComponent<Skill>();
    }

    private void HandleMouseMovement()
    {
        if (Mouse.current == null) return;
        if (skill != null && (!skill.CanAttack || !skill.CanPotion)) return;
        
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundLayer))
                {
                    ClearTarget();
                    agent.SetDestination(hit.point);
                    if (skill.IsTwinBladeActive)
                    {
                        animator.SetBool("isWalking", true);
                    Debug.Log("Twin Blade Walking");
                    }
                    else if (skill.IsDualSwordsActive)
                    {
                        animator.SetBool("D_isWalking", true);
                    Debug.Log("Dual Swords Walking");
                    }
                }
            }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200f))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    SetTarget(hit.collider.transform);
                    Debug.Log("Locked target: " + target.name);
                }
                else
                {
                    // ถ้าคลิกขวาโดนอย่างอื่นที่ไม่ใช่ศัตรู ให้ปลดล็อกเป้า
                    ClearTarget();
                }
            }
            else
            {
                // ถ้าคลิกขวาโดนความว่างเปล่า ปลดล็อกเป้า
                ClearTarget();
            }
        }
    }


    void Update()
    {
        HandleMouseMovement();
        UpdateAnimator();
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) <= Range)
            {
                RotateTowardsTarget();
            }
            
        }
    }

    private void UpdateAnimator()
    {
        if(agent.pathPending)
        {
            return;
        }
        
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0)
            {
                animator.SetBool("isWalking", false);
               // Debug.Log("StopWalking");
                animator.SetBool("D_isWalking", false);
            }
        }
    }

    public void StopMovement()
    {
        if (agent != null)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("D_isWalking", false);
        }
    }

    public void Target(Vector3 targetPosition)
    {
        if (agent != null)
        {
            agent.SetDestination(targetPosition);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0f; // Keep the rotation only on the horizontal plane
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }

    public void ClearTarget()
    {
        target = null;
    }
}

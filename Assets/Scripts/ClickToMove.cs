using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float stoppingDistanse = 0.2f;


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
        
            if (Mouse.current.leftButton.isPressed)
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundLayer))
                {
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
        
        

    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleMouseMovement();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        if(agent.pathPending)
        {
            return;
        }
        
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                animator.SetBool("isWalking", false);
                //Debug.Log("StopWalking");
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
}

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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        agent.stoppingDistance = stoppingDistanse;
    }

    private void HandleMouseMovement()
    {
        if (Mouse.current == null) return;
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200f, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        HandleMouseMovement();
    }
}

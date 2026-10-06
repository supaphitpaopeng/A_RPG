using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    [Header("Weapon Settings")]
    [SerializeField] private WeaponHitboxEnemy activeWeaponHitbox; // ใช้ตัวแปรนี้หลักๆ เลย

    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackTime = 0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        // ถ้าไม่ได้ลากใส่ช่อง Inspector ไว้ ให้ระบบลองหาจากลูกให้เองโดยอัติโนมัติ
        if (activeWeaponHitbox == null)
        {
            activeWeaponHitbox = GetComponentInChildren<WeaponHitboxEnemy>();
        }

        if (player != null)
        {
            Debug.Log("Player found: " + player.name);
        }
        else
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
            animator.SetBool("Walk", true);

            if (distanceToPlayer <= 1f)
            {
                RotateTowardsPlayer();
                StopMovement();

                // เรียกใช้ฟังก์ชันโจมตี
                AttackPlayer();
            }
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("Walk", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void AttackPlayer()
    {
        if (Time.time >= attackTime)
        {
            Debug.Log("Enemy attacks the player!");
            animator.SetTrigger("Attack");
            StopMovement();

            attackTime = Time.time + attackCooldown;
        }
    }

    public void StopMovement()
    {
        if (agent != null && agent.enabled)
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }
        if (animator != null)
        {
            animator.SetBool("Walk", false);
        }
    }

    // ฟังก์ชันเปิด-ปิด Hitbox (สามารถเอาไปใส่เป็น Animation Event ในจังหวะที่อนิเมชันโจมตีฟันโดนพอดีได้เลย)
    public void ActivateHitbox()
    {
        if (activeWeaponHitbox != null)
        {
            activeWeaponHitbox.EnableHitbox();
        }
    }

    public void DeactivateHitbox() // เอาเครื่องหมาย semicolon ออกแล้ว
    {
        if (activeWeaponHitbox != null)
        {
            activeWeaponHitbox.DisableHitbox();
        }
    }
}
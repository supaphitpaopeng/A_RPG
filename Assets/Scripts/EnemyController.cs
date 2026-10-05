using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Enemy Settings")]
    [SerializeField] private float detectionRange = 10f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player != null)
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
            if (distanceToPlayer <= 2.5)
            {
                agent.ResetPath();
                // Attack logic here
                Debug.Log("Enemy is attacking the player.");
            }
        }
        else
        {
            agent.ResetPath();
        }
        Debug.Log("Distance to player: " + distanceToPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);
    }

}

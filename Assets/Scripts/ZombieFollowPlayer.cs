using UnityEngine;
using UnityEngine.AI;

public class ZombieFollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target; // Drag your player here
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Required for 2D to prevent the agent from trying to rotate in 3D space
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(target.position);

    }

    void Update()
    {
        if (target != null)
        {
            // Update the agent's destination to the player's current position
            agent.SetDestination(target.position);
            Debug.Log("agent destination" + target.position);
        }
    }
}

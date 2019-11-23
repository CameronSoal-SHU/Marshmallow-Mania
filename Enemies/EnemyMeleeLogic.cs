using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeLogic : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] Animator animator = null;
    [SerializeField] float damage = 10;
    [SerializeField] float attackCooldown = 1.0f;
    private float timeUntilNextAttack = 0.0f;

    private void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void OnTriggerExit(Collider colliderObject)
    {
        if (colliderObject.tag == "Player") timeUntilNextAttack = 0;
    }

    private void OnTriggerStay(Collider colliderObject)
    {
        if (colliderObject.tag == "Player")
        {
            timeUntilNextAttack -= Time.deltaTime;
            if (timeUntilNextAttack <= 0)
            {
                PlayerAttributes targettedPlayerAttributes = colliderObject.gameObject.GetComponent<PlayerAttributes>();

                targettedPlayerAttributes.ChangeHp(-damage, false);
                if (animator != null) animator.SetTrigger("Attack");

                timeUntilNextAttack = attackCooldown;
            }
        }
        
        agent.velocity = Vector3.zero; // <- Stop the object from moving to prevent pushing
    }
}

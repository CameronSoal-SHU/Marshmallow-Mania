using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform { get; private set; }
    protected NavMeshAgent agent;
    HealthHandler health;
    CapsuleCollider capsuleCollider = null;
    BoxCollider boxCollider = null;

    private Room room;

    protected Animator animator;
    [SerializeField] float deathAnimationDelay = 1.0f;
    
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        room = GetComponentInParent<Room>();
        if (room == null)
        {
            Debug.LogWarning("Enemy " + name + " does not belong to a room.");
        }
        else
        {
            room.AddEnemy(this);
        }

        health = GetComponent<HealthHandler>();
        health.onDeath += Die;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!health.IsAlive) { agent.enabled = false; return; } // prevent enemy from following player after death

        agent.SetDestination(playerTransform.position);
        animator.SetFloat("MoveSpeed", agent.velocity.magnitude);
    }

    protected virtual void Die()
    {
        // prevent enemy harming player after death
        if (capsuleCollider != null) capsuleCollider.enabled = false;
        if (boxCollider != null) boxCollider.enabled = false;

        Destroy(gameObject, deathAnimationDelay);
        agent.velocity = Vector3.zero;

        if (room != null)
        {
            room.RemoveEnemy(this);
        }
    }
}

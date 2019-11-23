using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] BossShootingPattern[] shots = null;
    BossShootingPattern currentShot;
    public Transform shotSummonPoint;
    [SerializeField] float shotCooldown = 3.0f;
    float currentShotCooldown;
    [SerializeField] GameObject healthBar = null;

    private void OnEnable()
    {
        healthBar.SetActive(true);
    }

    protected override void Start()
    {
        base.Start();
        foreach (BossShootingPattern shot in shots)
        {
            shot.SetBoss(this);
        }
        Invoke("FireShot", shotCooldown);
    }

    private void Update()
    {
        // Chase player for now
        agent.SetDestination(playerTransform.position);

        // Handle the shots
        if (currentShot != null)
        {
            if (!currentShot.IsDone)
            {
                currentShot.Update();
            }
            else
            {
                currentShotCooldown -= Time.deltaTime;
                if (currentShotCooldown < 0)
                {
                    FireShot();
                }
            }
        }
    }

    void FireShot()
    {
        currentShotCooldown = shotCooldown;
        currentShot = shots[Random.Range(0,shots.Length)];
        currentShot.Start();
    }

    protected override void Die()
    {
        healthBar.SetActive(false); // prevent HP bar persisting after death
        base.Die();
    }
}

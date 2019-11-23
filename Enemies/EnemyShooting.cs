using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    [SerializeField] GameObject bulletPrefab = null;
    [SerializeField] AudioClip throwingSound = null;
    [SerializeField] float fireRate = 1.0f;
    [SerializeField] float animationOffset = 0.2f;

    bool canShoot = false;
    bool triggeredAnimation = false;
    float cooldown = -1.0f;
    AudioSource audioSource = null;

    [SerializeField] Transform bulletSummonPoint = null;

    protected override void Start()
    {
        base.Start();
        if (bulletSummonPoint == null) bulletSummonPoint = transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Face towards player
        Vector3 direction = playerTransform.position - bulletSummonPoint.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);

        if (!canShoot)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0) canShoot = true;

            if (cooldown - animationOffset < 0 && !triggeredAnimation)
            {
                triggeredAnimation = true;
                animator?.SetTrigger("Shoot");
            }
        }

        if (canShoot)
        {
            Fire();
        }
    }

    void Fire()
    {
        canShoot = false;
        cooldown = 1 / fireRate;
        audioSource.PlayOneShot(throwingSound);

        // Summon bullet

        //Quaternion bulletRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        Instantiate(bulletPrefab, bulletSummonPoint.position, transform.rotation * bulletPrefab.transform.rotation);

        triggeredAnimation = false;
    }
}

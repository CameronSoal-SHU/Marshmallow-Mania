using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField]
    GameObject bulletPrefab = null;
    [SerializeField] AudioClip shootingAudio = null;

    bool canShoot;
    float cooldown = -1.0f;

    Animator animator;
    AudioSource audioSource;
    PlayerAttributes playerAttributes;

    [SerializeField]
    Transform bulletSummonPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerAttributes = GetComponent<PlayerAttributes>();
        audioSource = GetComponent<AudioSource>();

        if (bulletSummonPosition == null) bulletSummonPosition = transform;
    }

    private void Update()
    {
        if (!canShoot)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0) canShoot = true;
        }

        if (Input.GetButton("Fire1") && canShoot)
        {
            Fire();
        }
    }

    void Fire()
    {
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(shootingAudio);

        canShoot = false;
        cooldown = 1 / playerAttributes.GetCurFireRate();
        // Summon bullet
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 pointInWorld = ray.GetPoint(distance);
            Vector3 direction = pointInWorld - transform.position;
            Quaternion bulletRotation = Quaternion.LookRotation(direction, Vector3.up);
            var instance = Instantiate(bulletPrefab, bulletSummonPosition.position, bulletRotation * bulletPrefab.transform.rotation);
            BulletBase firedBullet = instance.GetComponent<BulletBase>();

            firedBullet.SetDamage(playerAttributes.GetCurDamage());
            firedBullet.SetSpeed(playerAttributes.GetCurProjSpeed());
        }
        else
        {
            Debug.Log("Raycast from mouse pos failed in " + name);
        }
    }
}

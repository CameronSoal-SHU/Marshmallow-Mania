using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField] string tagToHit = "Enemy";
    [SerializeField] string tagToIgnore = "Player";

    [SerializeField] protected int damage = 0;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] float lifetime = 5.0f;
    [SerializeField] private AudioClip impactSound = null;

    [SerializeField] AudioSource audioSource = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToHit)
        {
            HealthHandler hp = other.GetComponent<HealthHandler>();
            if (hp)
            {
                hp.ChangeHp(-damage, false);
            }
            else
            {
                Debug.Log(name + ": does not have a HealthHandler");
            }
        }
        if (!other.isTrigger && other.tag != tagToIgnore)
        {
            AudioSource.PlayClipAtPoint(impactSound, GameObject.FindGameObjectWithTag("Player").transform.position);
            Destroy(gameObject);
            
            Debug.Log("Hit " + other.gameObject.name);
        }
    }

    protected virtual void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Destroy(gameObject, lifetime);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(int newDamageValue) { damage = newDamageValue; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected bool isNegativeEffect = false;

    protected virtual void OnTriggerEnter(Collider colliderObject)
    {
        if (colliderObject.CompareTag("Player"))
        {
            PlayerAttributes playerAttributes = colliderObject.gameObject.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                ApplyEffect(colliderObject.gameObject.GetComponent<PlayerAttributes>());
                playerAttributes.PlayPowerUpSound();
            }
            else
            {
                Debug.LogWarning("Could not find PlayerAtrributes on " + colliderObject.gameObject);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    protected abstract void ApplyEffect(PlayerAttributes playerAttributes);
}

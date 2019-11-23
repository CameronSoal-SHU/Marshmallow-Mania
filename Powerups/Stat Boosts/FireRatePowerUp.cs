using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : Powerup
{
    [SerializeField] [Range(0,10)] private float fireRateIncrease = 1.0f;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.SetFireRate(playerAttributes.GetCurFireRate() + (isNegativeEffect ? fireRateIncrease *= -1 : fireRateIncrease));
    }
}

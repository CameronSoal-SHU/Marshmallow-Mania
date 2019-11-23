using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    [SerializeField] protected int healthChange = 0;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.ChangeHp(isNegativeEffect ? healthChange * -1 : healthChange, true);
    }
}

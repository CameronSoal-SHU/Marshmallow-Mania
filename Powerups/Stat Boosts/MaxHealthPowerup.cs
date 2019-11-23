using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPowerup : Powerup
{
    [SerializeField] private int maxHealthChange = 0;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.ChangeMaxHealth(isNegativeEffect ? maxHealthChange * -1 : maxHealthChange);
    }
}

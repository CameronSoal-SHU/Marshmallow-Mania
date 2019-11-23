using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPowerup : Powerup
{
    [SerializeField] float firerateChange = 0;
    [SerializeField] int healthChange = 0;
    [SerializeField] int maxHealthChange = 0;
    [SerializeField] int armourChange = 0;
    [SerializeField] int damageChange = 0;
    [SerializeField] float moveSpeedChange = 0;
    [SerializeField] float projSpeedChange = 0;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.SetFireRate(playerAttributes.GetCurFireRate() + firerateChange);
        playerAttributes.ChangeHp(healthChange, true);
        playerAttributes.ChangeMaxHealth(maxHealthChange);
        playerAttributes.SetMoveSpeed(playerAttributes.GetCurMoveSpeed() + moveSpeedChange);
        playerAttributes.SetProjSpeed(playerAttributes.GetCurProjSpeed() + projSpeedChange);
        playerAttributes.ChangeDamage(damageChange);
        playerAttributes.ChangeArmour(armourChange);
    }
}

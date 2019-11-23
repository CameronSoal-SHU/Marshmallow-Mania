using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjSpeedPowerUp : Powerup
{
    [SerializeField] private float ProjSpeedChange = 0.0f;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.SetProjSpeed(playerAttributes.GetCurProjSpeed() + (isNegativeEffect ? ProjSpeedChange *= -1 : ProjSpeedChange));
    }
}

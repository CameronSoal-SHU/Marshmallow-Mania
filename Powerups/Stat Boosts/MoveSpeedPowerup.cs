using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedPowerup : Powerup
{
    [SerializeField] [Range(1,99)] private float playerSpeedChange = 1.0f;

    protected override void ApplyEffect(PlayerAttributes playerAttributes)
    {
        playerAttributes.SetMoveSpeed(playerAttributes.GetCurMoveSpeed() + (isNegativeEffect ? playerSpeedChange * -1 : playerSpeedChange));
    }
}

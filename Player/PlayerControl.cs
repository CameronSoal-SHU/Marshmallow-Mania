using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick = null;

    [SerializeField]
    public float player_speed = 10.0f;

    void Update()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(joystick.Horizontal*player_speed, 
            rigidbody.velocity.y, 
            joystick.Vertical*player_speed);

    }
}

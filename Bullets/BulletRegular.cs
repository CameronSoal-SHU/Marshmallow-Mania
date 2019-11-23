using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRegular : BulletBase
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Room>() != null)
        {
            // this prevents killing disabled enemies across rooms
            damage = 0;
        }
    }

    protected override void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}

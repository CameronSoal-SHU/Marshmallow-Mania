using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class EnemySplitting : Enemy
{
    [SerializeField]
    GameObject splitInto = null;

    [SerializeField]
    int splitAmount = 2;

    protected override void Die()
    {
        base.Die();
        for (int i = 0; i < splitAmount; i++)
        {
            Instantiate(splitInto, transform.position, splitInto.transform.rotation, transform.parent).GetComponent<Enemy>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossShootingPattern : ScriptableObject
{
    protected Boss boss;
    public bool IsDone { get; protected set; }

    public void SetBoss(Boss boss)
    {
        this.boss = boss;
    }

    public virtual void Update() { }

    public virtual void Start() { }
}

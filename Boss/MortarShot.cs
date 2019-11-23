using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Mortar Shot", menuName = "Boss/Mortar Shot")]
public class MortarShot : BossShootingPattern
{
    [SerializeField] GameObject shotPrefab = null;
    [SerializeField] Vector3 summonOffset = Vector3.up;

    public override void Start()
    {
        IsDone = true;
        Fire(boss.playerTransform.position);
    }

    protected BulletArced Fire(Vector3 target)
    {
        GameObject instance = Instantiate(shotPrefab, boss.shotSummonPoint.position + summonOffset, shotPrefab.transform.rotation);
        BulletArced bullet = instance.GetComponent<BulletArced>();
        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
        else
        {
            Debug.LogWarning("Mortar shot prefab doesn't have a BulletArced script");
        }
        return bullet;
    }
}

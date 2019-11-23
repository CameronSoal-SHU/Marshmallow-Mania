using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Multi Shot", menuName = "Boss/Multi Shot")]
public class MultiShot : BossShootingPattern
{
    [SerializeField] GameObject shotPrefab = null;
    [SerializeField] int noOfShots = 1;
    [SerializeField] float angle = 0;

    public override void Start()
    {
        IsDone = true;
        for (int i = 0; i < noOfShots; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 360f * i / noOfShots + angle, 0);
            Instantiate(shotPrefab, boss.shotSummonPoint.position, rot * shotPrefab.transform.rotation);
        }
    }
}

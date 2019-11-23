using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spiral Shot", menuName = "Boss/Spiral Shot")]
public class SpiralShot : BossShootingPattern
{
    [SerializeField] protected GameObject shotPrefab;
    [SerializeField] protected int noOfShots;
    [SerializeField] protected float angle = 0;
    protected int currentShot;
    [SerializeField] protected float timeBetweenShots;
    protected float currentTimeBetweenShots;

    public override void Start()
    {
        IsDone = false;
        currentShot = 0;
        FireShot();
    }

    public override void Update()
    {
        currentTimeBetweenShots -= Time.deltaTime;
        if (currentTimeBetweenShots < 0)
        {
            FireShot();
        }
    }

    protected virtual void FireShot()
    {
        currentTimeBetweenShots = timeBetweenShots;
        Quaternion rot = Quaternion.Euler(0, 360f * currentShot / noOfShots + angle, 0);
        Instantiate(shotPrefab, boss.shotSummonPoint.position, rot * shotPrefab.transform.rotation);

        currentShot++;
        if (currentShot == noOfShots)
        {
            IsDone = true;
        }
    }
}

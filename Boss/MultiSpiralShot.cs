using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Multi Spiral Shot", menuName = "Boss/Multi Spiral Shot")]
public class MultiSpiralShot : SpiralShot
{
    [SerializeField] int noOfSpirals = 4;

    protected override void FireShot()
    {
        currentTimeBetweenShots = timeBetweenShots;

        for (int i = 0; i < noOfSpirals; i++)
        {
            //360f * currentShot / noOfShots
            Quaternion rot = Quaternion.Euler(0, 360f * currentShot / (noOfShots * noOfSpirals) + 360f * i / noOfSpirals + angle, 0);
            Instantiate(shotPrefab, boss.shotSummonPoint.position, rot * shotPrefab.transform.rotation);
        }

        currentShot++;
        if (currentShot == noOfShots)
        {
            IsDone = true;
        }
    }
}

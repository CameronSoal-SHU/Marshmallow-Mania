using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scatter Mortar Shot", menuName = "Boss/Scatter Mortar Shot")]
public class ScatterMortarShot : MortarShot
{
    [SerializeField] int noOfShots = 5;
    int currentShot;
    [SerializeField] float minSpeed = 5.0f;
    [SerializeField] float maxSpeed = 15.0f;
    [SerializeField] float timeBetweenShots = 1.0f;
    float currentTimeBetweenShots;
    [SerializeField] float radius = 2.0f;

    public override void Start()
    {
        IsDone = false;
        currentShot = 0;

        Debug.Log("Start");
    }

    public override void Update()
    {
        currentTimeBetweenShots -= Time.deltaTime;
        if (currentTimeBetweenShots < 0)
        {
            FireScatter();
        }
    }

    void FireScatter()
    {
        Debug.Log("Fire");
        Vector2 randomPos = Random.insideUnitCircle * radius;
        Vector3 target = new Vector3(
            boss.playerTransform.position.x + randomPos.x,
            boss.playerTransform.position.y,
            boss.playerTransform.position.z + randomPos.y);

        Fire(target).SetSpeed(Random.Range(minSpeed, maxSpeed));

        currentTimeBetweenShots = timeBetweenShots;
        currentShot++;
        if (currentShot == noOfShots)
        {
            IsDone = true;
        }
    }
}

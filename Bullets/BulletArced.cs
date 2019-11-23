using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArced : BulletBase
{
    [SerializeField] float height = 5.0f;
    Vector3 target;
    //[SerializeField] float m_MinArcSize = 2.0f;

    float timeSinceExisting = 0;
    Vector3 direction;
    Vector3 startingPos;

    protected override void Start()
    {
        startingPos = transform.position;
        direction = target - transform.position;
        //Debug.Log(direction);
        //if (direction.magnitude < m_MinArcSize)
        //{
        //    direction = direction.normalized * m_MinArcSize;
        //}
    }

    void Update()
    {
        timeSinceExisting += Time.deltaTime;
        float t = timeSinceExisting / direction.magnitude * speed;
        transform.position = EvaluatePosition(t);
    }

    Vector3 EvaluatePosition(float t)
    {
        return startingPos + direction * t + Vector3.up * EvaluateParabola(t);
    }

    float EvaluateParabola(float t)
    {
        return (1 - t) * t * height;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        direction = target - transform.position;
    }
}

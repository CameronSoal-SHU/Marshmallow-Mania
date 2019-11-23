using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnDeath : MonoBehaviour
{
    [SerializeField] GameObject[] splitInto = null;
    [SerializeField] float delay = 2.0f;

    void Start()
    {
        GetComponent<HealthHandler>().onDeath += Die;
    }

    void Die()
    {
        Invoke("Split", delay);
    }

    void Split()
    {
        foreach (GameObject spawn in splitInto)
        {
            Instantiate(spawn, transform.position, spawn.transform.rotation, transform.parent);
        }
    }
}

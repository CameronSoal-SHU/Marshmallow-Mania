﻿using UnityEngine;

public class leftBossWinCondition : MonoBehaviour
{
    private GameObject playerGO = null;

    void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<HealthHandler>().GetCurHealth() <= 0)
            playerGO.GetComponent<PlayerController>().LeftBossKilled();
    }
}

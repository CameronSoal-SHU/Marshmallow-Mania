using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [Header("Health/Armour/Score Displays")]
    [SerializeField] RectTransform healthForeground = null;
    [SerializeField] Text healthCount = null;
    [SerializeField] RectTransform armourForeground = null;
    [SerializeField] Text scoreCount = null;

    [Header("Target Health/Attributes Handler")]
    [SerializeField] HealthHandler health = null;
    [SerializeField] PlayerAttributes playerAttributes = null;

    void Update()
    {
        if (healthForeground != null) healthForeground.localScale = new Vector3(health.GetCurHealth() / health.GetCurMaxHealth(), healthForeground.localScale.y, healthForeground.localScale.z);
        if (armourForeground != null) armourForeground.localScale = new Vector3(health.GetCurArmour() / 100.0f, armourForeground.localScale.y, armourForeground.localScale.z);
        if (healthCount != null) healthCount.text = health.GetCurHealth().ToString() + "/" + health.GetCurMaxHealth().ToString() + " | " + health.GetCurArmour();
        if (scoreCount != null && playerAttributes != null) scoreCount.text = "Score:\n" + playerAttributes.GetCurPlayerScore().ToString();
    }
}

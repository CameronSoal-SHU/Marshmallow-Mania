using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickupLogic : MonoBehaviour
{
    [SerializeField] private int armourLevel = 0;

    private void OnTriggerEnter(Collider colliderObject)
    {
        if(colliderObject.tag == "Player") // Check if player touched it
        {
            // Get access to player stats
            GameObject player = colliderObject.gameObject;
            PlayerAttributes playerHealth = player.GetComponent<PlayerAttributes>();

            // Check armour level assigned to pickup
            switch (armourLevel)
            {
                case 1:
                    playerHealth.ChangeArmour(25);
                    break;

                case 2:
                    playerHealth.ChangeArmour(50);
                    break;

                case 3:
                    playerHealth.ChangeArmour(75);
                    break;

                default: // reset armour to 0
                    playerHealth.ChangeArmour(-150);
                    break;
            }
            Destroy(gameObject); // Remove from game scene
        }
    }
}

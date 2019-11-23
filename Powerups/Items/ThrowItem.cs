
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowItem : MonoBehaviour
{
    [SerializeField] private GameObject AreaOfEffectPrefab = null;

    void Update()
    {
        if(transform.position.y <= 0f)                          // test if object has hit the floor
        {
            Debug.Log("HIT THE GROUND");
            Instantiate(AreaOfEffectPrefab,                     // spawn AOE
                new Vector3(transform.position.x, 0, transform.position.z), 
                Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class InventoryLogic : MonoBehaviour
{
    [SerializeField] private GameObject storedObject;

    [SerializeField] private KeyCode useItem = KeyCode.F;
    [SerializeField] private KeyCode dropItem = KeyCode.Q;

	[SerializeField] private Vector3 spawnTranslation = new Vector3(0f, 1f, 0f);

    [SerializeField] private float itemSpeed = 10.0f;
    [SerializeField] private GameObject AreaOfEffectPrefab;

    private bool pickUpable = true;

    protected void OnTriggerEnter(Collider colliderObject)
    {
        if (pickUpable && colliderObject.CompareTag("PickupItem"))
        {
            Debug.Log("Item picked up: TEST");
            storedObject = colliderObject.transform.parent.gameObject;
            colliderObject.transform.parent.gameObject.SetActive(false);
            colliderObject.transform.parent.transform.SetParent(gameObject.transform);
        }
    }

    protected void OnTriggerExit(Collider colliderObject)
    {
        pickUpable = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dropItem))
        {
            if (storedObject != null)
            {
                float disableHitBoxStartTime = Time.time;
                Collider pickupItemHitbox = storedObject.GetComponent<Collider>();

				DropItem();
            }
        }
		if (Input.GetKeyDown(useItem))
		{
			if (storedObject != null)
			{
				pickUpable = false; // prevent player from picking up the item instantly

				ThrowItem();
			}
		}
    }

	private void DropItem()
	{
		storedObject.SetActive(true);
		storedObject.transform.SetParent(null); // make object independant from Sof again
		storedObject.transform.position = transform.position; // drop item at Sof's feet
		storedObject.transform.rotation = Quaternion.Euler(0, 0, 0); // reset dropped item rotation for consistency 
		pickUpable = false;
	}

	private void ThrowItem()
	{
		Plane plane = new Plane(Vector3.up, transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (plane.Raycast(ray, out float distance))
		{
			// find position and direction to spawn item
			Vector3 pointInWorld = ray.GetPoint(distance);
			Vector3 direction = pointInWorld - transform.position;
			Quaternion itemRotation = Quaternion.LookRotation(direction, Vector3.up);

			storedObject.SetActive(true);
			// Instantiate the object to be thrown
			GameObject ThrowItemClone = Instantiate(storedObject, 
                transform.position + spawnTranslation, 
                itemRotation);

            addThrowVelocity(ThrowItemClone);

			// permanently remove item from player
			Destroy(storedObject);

			storedObject = null; // remove object from inventory
		}
		else
		{
			Debug.Log("Raycast from mouse pos failed in " + name);
		}
	}

    void addThrowVelocity(GameObject ThrowObject)
    {
        // add physics to throwing object
        ThrowObject.GetComponent<Rigidbody>().useGravity = true;
        ThrowObject.GetComponent<Rigidbody>().isKinematic = false;
        ThrowObject.GetComponent<Rigidbody>().velocity = itemSpeed * transform.forward;
    }
}

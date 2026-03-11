using UnityEngine;


// Detects interactable items in front of the player using a raycast
// and triggers the item inspection system when the player presses the interaction key.

public class PickupDetector : MonoBehaviour
{
    // Maximum distance at which the player can interact with items
    public float pickupRange = 3f;

    // Layer used to filter objects that can be picked up
    public LayerMask pickupLayer;

    // Reference to the inspection system that handles item viewing
    public ItemInspectionManager inspectionManager;

    void Update()
    {
        // Check if interaction key was pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Create a ray from the camera forward
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // Perform raycast to detect objects within pickup range
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer))
            {
                // Check if the hit object has a PickupableItem component
                PickupableItem item = hit.collider.GetComponent<PickupableItem>();

                if (item == null)
                {
                    return;
                }

                // Send the item to the inspection system
                inspectionManager.EnterInspection(item);

            }
        }
    }
}


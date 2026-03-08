using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    // Prefab used for displaying the item in inspection mode
    public GameObject itemPrefabForInspection;
    
    // UI text shown when the player can interact with the item
    public GameObject text;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger area
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            
            // Show interaction prompt
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the trigger area
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            
            // Hide interaction prompt
            text.SetActive(false);
        }
    }

    public void HideTextAfterInspection()
    {
        // Ensures the interaction text is hidden after item inspection
        if (text != null)
        {
            text.SetActive(false);
        }
    }
}




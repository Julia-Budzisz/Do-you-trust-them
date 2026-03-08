using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class ItemInspectionManager : MonoBehaviour
{
    // UI displayed during item inspection
    public GameObject inspectionUI;

    // Camera used to render inspected objects
    public Camera inspectionCamera;

    // Position where inspected items will appear
    public Transform itemSpawnPoint;

    // Speed of item rotation when using the mouse
    public float rotationSpeed = 5f;

    // Currently inspected item reference
    public PickupableItem currentItem;

    public MenuView menuView;

    // Player movement scripts that will be disabled during inspection
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;

    // Instantiated object used for visual inspection
    public Transform inspectedItem;

    // Flag indicating if inspection mode is active
    public bool isInspecting = false;

    // Reference to audio manager
    public AudioManager audioManager;


    void Update()
    {
        // Allow item rotation only when inspecting
        if (isInspecting && inspectedItem != null)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed;

            // Rotate item based on mouse movement
            inspectedItem.Rotate(Vector3.up, -rotX, Space.World);
            inspectedItem.Rotate(Vector3.right, rotY, Space.Self);

            // Exit inspection when Escape is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitInspection();
            }
        }
    }

    public void EnterInspection(PickupableItem item, bool fromButton)
    {
       
        isInspecting = true;
      
        Time.timeScale = 1f; // Ensure time is not scaled down during inspection

        currentItem = item;

        // Spawn inspection version of the item
        inspectedItem = Instantiate(item.itemPrefabForInspection).transform;
        inspectedItem.position = itemSpawnPoint.position;
        inspectedItem.rotation = Quaternion.identity;

        // Enable inspection UI and camera
        inspectionUI.SetActive(true);
        inspectionCamera.gameObject.SetActive(true);

        // Disable player controls
        playerMovement.enabled = false;
        cameraMovement.enabled = false;

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        // Disable menu access during inspection
        menuView.SetAccess(false);

        if (audioManager != null)
        {
            audioManager.PlayPickupSound();
            audioManager.MuteSteps();
        }
    }

    public void ExitInspection()
    {
        isInspecting = false;

        // Destroy inspected object
        Destroy(inspectedItem.gameObject);
        inspectedItem = null;

        // Disable inspection UI and camera
        inspectionUI.SetActive(false);
        inspectionCamera.gameObject.SetActive(false);

        // Re-enable player controls
        playerMovement.enabled = true;
        cameraMovement.enabled = true;

        // Hide item interaction text after inspection

        if (currentItem != null)
        {
            currentItem.HideTextAfterInspection();
        }

        currentItem = null;

        // Lock cursor back to gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        menuView.SetAccess(true);

        // Restore footstep sounds
        audioManager.UnmuteSteps();
    }

}

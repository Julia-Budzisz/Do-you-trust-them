using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Controls printer interaction, including switching cameras,
// displaying printed images in UI slots, and managing player interaction state.

public class Printer : MonoBehaviour
{
    // Main gameplay camera
    public Camera playerCamera;

    // Camera used for printer interaction view
    public Camera printerCamera;

    // UI canvas displayed during printer interaction
    public GameObject printerCanvas;

    // Reference to menu manager (to disable menu during interaction)
    public MenuView menuView;

    // Indicates whether the player is currently using the printer
    private bool isInPrinterView = false;

    // Indicates whether the player is close enough to interact
    private bool isPlayerInRange = false;

    // UI slots where printed images will appear
    public List<Image> imageSlots;

    // Interaction prompt shown when player approaches
    public GameObject text;

    // Audio manager reference
    public AudioManager audioManager;

    void Start()
    {
        // Disable printer camera at start
        printerCamera.enabled = false;

        // Hide printer UI
        if (printerCanvas != null)
        {
            printerCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // Enter printer view when player presses F near the printer
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isInPrinterView)
        {
            EnterPrinterView();
        }
        // Exit printer view with Escape
        else if (Input.GetKeyDown(KeyCode.Escape) && isInPrinterView)
        {
            ExitPrinterView();
        }
    }

    public void EnterPrinterView()
    {
        playerCamera.enabled = false;
        printerCamera.enabled = true;

        isInPrinterView = true;

        // Disable menu access during printer interaction
        menuView.SetAccess(false);

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

      
         printerCanvas.SetActive(true);
        

        // Play printer sound and mute footsteps
        if (audioManager != null)
        {
            audioManager.PlayPrinterSound();
            audioManager.MuteSteps();
        }
    }


    void ExitPrinterView()
    {
        // Restore cameras
        printerCamera.enabled = false;
        playerCamera.enabled = true;

        isInPrinterView = false;

        // Restore menu access
        menuView.SetAccess(true);

        // Lock cursor back to gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        printerCanvas.SetActive(false);

        // Restore footsteps sound
        if (audioManager != null)
        {
            audioManager.UnmuteSteps();
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if player enters interaction range
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            isPlayerInRange = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detect if player leaves interaction range
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            isPlayerInRange = false;
            text.SetActive(false);
        }
    }

    public void SetPrinterImages(List<Sprite> sprites)
    {
        // Assign sprites to printer UI slots
        for (int i = 0; i < imageSlots.Count; i++)
        {
            if (i < sprites.Count && sprites[i] != null)
            {
                imageSlots[i].sprite = sprites[i];
                imageSlots[i].gameObject.SetActive(true);
            }
            else
            {
                imageSlots[i].sprite = null;
                imageSlots[i].gameObject.SetActive(false);
            }
        }
    }
}

using UnityEngine;

//Controls pause menu and its access 

public class MenuView : MonoBehaviour
{
    // UI panel containing the pause menu
    [SerializeField] private GameObject menuPanel;

    // Reference to the audio manager for controlling sound
    [SerializeField] private AudioManager audioManager;

    // Determines if the menu can currently be opened
    public bool isMenuAvailable = true;

    // Tracks whether the menu is currently open
    private bool isMenuOpen = false;
    
    void Update()
    {
        // Open the menu when Escape is pressed and the menu is not already open
        if (Input.GetKeyDown(KeyCode.Escape) && !isMenuOpen)
        {
            OpenMenu();
        }
    }

    private void OpenMenu()
    {
        // Prevent opening the menu if access is disabled
        if (!isMenuAvailable) return;

        // Show menu UI
        menuPanel.SetActive(true);
        isMenuOpen = true;

        // Pause game time
        Time.timeScale = 0f;

        // Unlock and show cursor for UI interaction
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;

        // Pause ambient sound and mute footsteps
        audioManager.AmbientSource.Pause();
        audioManager.MuteSteps();
        
    }

    public void CloseMenu()
    {
        // Hide menu UI
        menuPanel.SetActive(false);
        isMenuOpen = false;

        // Resume normal game time
        Time.timeScale = 1f;

        // Lock cursor back to gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume ambient sound and footsteps
        audioManager.AmbientSource.UnPause();
        audioManager.UnmuteSteps();
    }

    public void SetAccess(bool access)
    {
        // Enables or disables the ability to open the menu
        isMenuAvailable = access;
    }
}

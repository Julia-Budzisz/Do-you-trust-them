using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Handles cabinet interaction system including code input puzzle,
// camera switching, UI management, and unlocking the cabinet when the correct code is entered. 
public class Cabinet : MonoBehaviour
{
    // Player objects that should be disabled during cabinet interaction
    public GameObject playerCamera;
    public PlayerMovement playerMovement;
    public MenuView menuView;

    // Objects used during cabinet interaction
    public GameObject cabinetCamera;
    public GameObject codeCanvas;
    public GameObject cabinetCanvas;
    public GameObject text;
    
    private bool isPlayerInRange = false;
    private bool isLocked = true;
    private bool isCabinetOpen = false;


    // Correct code for unlocking the cabinet
    public int[] correctCode = { 9, 4, 3, 1, 9 };

    // Current player input code
    private int[] currentCode = new int[5];

    // UI elements displaying digits
    public TextMeshProUGUI[] digitText;

    // Button used to confirm code
    public Button enterButton;

    // Currently selected digit index
    private int currentDigitIndex = 0;


    public void Start()
    {
        // Disable cabinet camera and UI at start
        cabinetCamera.SetActive(false);

        cabinetCanvas.SetActive(false);
        codeCanvas.SetActive(false);

        // Register button listener
        enterButton.onClick.AddListener(CheckCode);
    }

    public void Update()
    {
        // Allow player to start cabinet interaction
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isCabinetOpen)
        {
            EnterCabinetCode();
           
        }

        // Handle code input if cabinet is locked
        if (isLocked)
        {
            SwitchBetweenDigits();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitCabinetCode();
                
            }
        }

        // Allow exit if cabinet is open
        else if (isCabinetOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitCabinetView();
            Cursor.visible = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Detect player entering interaction range
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            isPlayerInRange = true;
            text.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Detect player leaving interaction range
        if (other.TryGetComponent<CharacterController>(out var controller))
        {
            isPlayerInRange = false;
            text.SetActive(false);
        }
    }

    public void EnterCabinetCode()
    {
        // Switch cameras to cabinet view
        playerCamera.SetActive(false);
        cabinetCamera.SetActive(true);

        // Disable player controls
        playerMovement.enabled = false;
        menuView.isMenuAvailable = false;

        // Show code input UI
        codeCanvas.SetActive(true);

        isLocked = true;

        // Unlock cursor for UI interaction
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitCabinetCode()
    {
        // Restore player camera
        cabinetCamera.SetActive(false);
        playerCamera.SetActive(true);

        // Re-enable player movement
        playerMovement.enabled = true;
        menuView.isMenuAvailable = true;

        // Hide code UI
        codeCanvas.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

    }

    public void EnterCabinetView()
    {

        // Switch to cabinet interior view
        playerCamera.SetActive(false);
        cabinetCamera.SetActive(true);

        playerMovement.enabled = false;
        menuView.isMenuAvailable = false;

        cabinetCanvas.SetActive(true);

        // Cabinet is now unlocked
        isLocked = false;
        isCabinetOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitCabinetView()
    {
        // Restore player state
        cabinetCamera.SetActive(false);
        playerCamera.SetActive(true);

        playerMovement.enabled = true;
        menuView.isMenuAvailable = true;

        cabinetCanvas.SetActive(false);
        isCabinetOpen = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

    }

    public void CheckCode()
    {
        // Compare player input with the correct code
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (currentCode[i] != correctCode[i]) return;
        }

        // Unlock cabinet if code matches
        EnterCabinetView();
        codeCanvas.SetActive(false);

    }

    public void SwitchBetweenDigits()
    {
        // Change digit value using mouse scroll
        if (Input.mouseScrollDelta.y != 0)
        {
           int digit = Input.mouseScrollDelta.y > 0 ? 1 : -1;
           currentCode[currentDigitIndex] = (currentCode[currentDigitIndex] + digit + 10) % 10;
           UpdateDigit();
        }

        // Move selection to next digit
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDigitIndex = (currentDigitIndex + 1) % 5;
            UpdateDigit();
        }

        // Move selection to previous digit
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDigitIndex = (currentDigitIndex + 4) % 5;
            UpdateDigit();
        }
    }

    public void UpdateDigit()
    {
        // Update digit UI and highlight currently selected digit
        for (int i = 0; i < currentCode.Length; i++)
        {
            digitText[i].text = currentCode[i].ToString();
            digitText[i].color = currentDigitIndex == i ? Color.yellow : Color.white;
        }
    }
}

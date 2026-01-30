using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseSystem : MonoBehaviour
{
    private PlayerController controller;
    public GameObject pauseMenu;
    private bool ispaused;

    private PlayerInput playerInput;

    private void Awake()
    {
        controller = FindAnyObjectByType<PlayerController>();
        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        TogglePause();
    }

    public void TogglePause()
    {
        ispaused = !ispaused;

        Time.timeScale = ispaused ? 0f : 1f;
        pauseMenu.SetActive(ispaused);
        controller.enabled = !ispaused;
        /*if (ispaused)
            playerInput.SwitchCurrentActionMap("UI");
        else
            playerInput.SwitchCurrentActionMap("Player");*/
    }
}

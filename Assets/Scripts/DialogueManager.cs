using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Transform player;

    [Header("UI")]
    public GameObject dialogueUI;
    public TMP_Text dialogueText;

    [Header("Input")]
    public PlayerInput playerInput;

    [Header("Camera")]
    public CameraZoomController zoomController;

    private string[] currentLines;
    private int currentIndex;
    private bool isTalking = false;

    void Awake()
    {
        Instance = this;
    }

    public bool IsTalking()
    {
        return isTalking;
    }

    public void StartDialogue(NPCInteraction npc)
    {
        isTalking = true;

        dialogueUI.SetActive(true);

        // troca pro mapa de UI
        playerInput.SwitchCurrentActionMap("UI");

        currentLines = npc.dialogueData.lines;
        currentIndex = 0;

        ShowLine();

        zoomController.ZoomIn(playerInput.transform, npc.transform);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!isTalking) return;

        NextLine();
    }

    public void NextLine()
    {
        currentIndex++;

        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void ShowLine()
    {
        dialogueText.text = currentLines[currentIndex];
    }

    public void EndDialogue()
    {
        isTalking = false;

        dialogueUI.SetActive(false);

        // volta pro controle do player
        playerInput.SwitchCurrentActionMap("Player");

        zoomController.ZoomOut();
    }
}
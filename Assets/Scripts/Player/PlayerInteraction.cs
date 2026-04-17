using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private NPCInteraction currentNPC;

    // chamado automaticamente pelo Input System
    public void OnInteract()
    {
        if (DialogueManager.Instance.IsTalking())
        {
            DialogueManager.Instance.NextLine();
            return;
        }

        if (currentNPC != null)
        {
            DialogueManager.Instance.StartDialogue(currentNPC);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPCInteraction>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
        }
    }
}
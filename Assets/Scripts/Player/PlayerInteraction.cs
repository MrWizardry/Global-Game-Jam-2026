using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private NPCInteraction currentNPC;

    [Header("Tags válidas para interação")]
    public List<string> npcTags;

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
        if (IsValidTag(other.tag))
        {
            currentNPC = other.GetComponent<NPCInteraction>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTag(other.tag))
        {
            currentNPC = null;
        }
    }

    private bool IsValidTag(string tag)
    {
        return npcTags.Contains(tag);
    }
}
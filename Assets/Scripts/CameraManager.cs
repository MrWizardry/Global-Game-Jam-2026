using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public CinemachineCamera dialogueCamera;

    void Awake()
    {
        Instance = this;
    }

    public void FocusDialogue(Transform player, Transform npc)
    {
        Vector3 midPoint = (player.position + npc.position) / 2f;

        dialogueCamera.transform.position = midPoint + new Vector3(0, 2, -2);

        dialogueCamera.LookAt = npc;
        dialogueCamera.Priority = 100;
    }

    public void ExitDialogue()
    {
        dialogueCamera.Priority = 0;
    }

}

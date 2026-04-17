using UnityEngine;
using Unity.Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    private CinemachineCamera cam;
    private Vector3 originalPosition;

    public float zoomOffset = 2f;

    public void ZoomIn(Transform player, Transform npc)
    {
        if (player == null || npc == null)
        {
            Debug.LogError("PLAYER OU NPC NULL");
            return;
        }

        CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();

        if (brain == null)
        {
            Debug.LogError("SEM CINEMACHINE BRAIN");
            return;
        }

        cam = brain.ActiveVirtualCamera as CinemachineCamera;

        if (cam == null)
        {
            Debug.LogError("CAMERA ATIVA NULL");
            return;
        }

        originalPosition = cam.transform.position;

        Vector3 midPoint = (player.position + npc.position) / 2f;
        Vector3 direction = (midPoint - cam.transform.position).normalized;

        cam.transform.position += direction * zoomOffset;

        Debug.Log("ZOOM OK");
    }

    public void ZoomOut()
    {
        if (cam == null) return;

        cam.transform.position = originalPosition;
    }
}
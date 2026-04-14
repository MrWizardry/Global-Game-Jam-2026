using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
public class SwitchCamera : MonoBehaviour
{
    public Transform jogador;
    public CinemachineCamera cameraAtiva;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAtiva.Priority = 1; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAtiva.Priority = 0;
        }
    }
}

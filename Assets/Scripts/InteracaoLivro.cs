using UnityEngine;
using Unity.Cinemachine;
using TMPro;
using UnityEngine.UI;

public class InteracaoLivro : MonoBehaviour
{
    public Transform jogador;
    public CinemachineCamera cameraAtiva;


    public Button interacaoEstanteButton;
    public Button interacaoSairEstanteButton;

    private void Start()
    {
        interacaoEstanteButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interacaoEstanteButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            interacaoEstanteButton.gameObject.SetActive(false);

        }
    }

    public void OlharEstante()
    {
        cameraAtiva.Priority = 1;
        interacaoEstanteButton.gameObject.SetActive(false);
        interacaoSairEstanteButton.gameObject.SetActive(true);
    }


    public void SairEstante()
    {
        cameraAtiva.Priority = 0;
        interacaoSairEstanteButton.gameObject.SetActive(false);
    }
}

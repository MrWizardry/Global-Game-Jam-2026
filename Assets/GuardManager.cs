using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public enum AlertStage
{
    Curioso,
    Investigando,
    Alerta
}

public class GuardManager : MonoBehaviour
{
    public float fov;
    [Range(0, 360)] public float fovAngle;

    public AlertStage alertStage;
    [Range(0, 200)] public float alertLevel;

    public GameObject inimigo;
    public float tempoAtivoInimigo = 10f;

    private Vector3 inimigoPosicaoInicial;
    public float tempoForaDoFOV = 0f;

    public AudioSource audioSource;
    [Range(0f, 1f)] public float volumeMinimo = 0f;
    [Range(0f, 1f)] public float volumeMaximo = 1f;

    private void Awake()
    {
        alertStage = AlertStage.Curioso;
        alertLevel = 0;

        inimigoPosicaoInicial = inimigo.transform.position;
        inimigo.SetActive(false);

        if (audioSource != null)
        {
            audioSource.volume = volumeMinimo;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void Update()
    {
        bool playerInFOV = false;

        Collider[] targetsInFOV = Physics.OverlapSphere(transform.position, fov);

        foreach (Collider collider in targetsInFOV)
        {
            if (collider.CompareTag("Player"))
            {
                float angle = Vector3.Angle(
                    transform.forward,
                    collider.transform.position - transform.position
                );

                if (angle < fovAngle / 2f)
                {
                    playerInFOV = true;
                    break;
                }
            }
        }

        UpdateAlertState(playerInFOV);
        AtualizarVolumeAudio();
    }

    private void AtualizarVolumeAudio()
    {
        if (audioSource == null) return;

        float volumeAlvo;

        switch (alertStage)
        {
            case AlertStage.Curioso:
                volumeAlvo = volumeMinimo;
                break;

            case AlertStage.Investigando:
                // Volume proporcional ao alertLevel (0 a 200)
                float progresso = alertLevel / 200f;
                volumeAlvo = Mathf.Lerp(volumeMinimo, volumeMaximo * 0.7f, progresso);
                break;

            case AlertStage.Alerta:
                volumeAlvo = volumeMaximo;
                break;

            default:
                volumeAlvo = volumeMinimo;
                break;
        }

        // Transição suave de volume
        audioSource.volume = Mathf.Lerp(audioSource.volume, volumeAlvo, Time.deltaTime * 5f);
    }

    private void UpdateAlertState(bool playerInFOV)
    {
        switch (alertStage)
        {
            case AlertStage.Curioso:
                if (playerInFOV)
                    alertStage = AlertStage.Investigando;
                break;

            case AlertStage.Investigando:
                if (playerInFOV)
                {
                    alertLevel += Time.deltaTime * 60f;

                    if (alertLevel >= 200)
                        alertStage = AlertStage.Alerta;
                }
                else
                {
                    alertLevel -= Time.deltaTime * 60f;

                    if (alertLevel <= 0)
                    {
                        alertLevel = 0;
                        alertStage = AlertStage.Curioso;
                    }
                }
                break;

            case AlertStage.Alerta:

                inimigo.SetActive(true);

                if (playerInFOV)
                {
                    tempoForaDoFOV = 0f;
                }
                else
                {
                    tempoForaDoFOV += Time.deltaTime;

                    if (tempoForaDoFOV >= tempoAtivoInimigo)
                    {
                        DesativarInimigo();
                    }
                }

                break;
        }
    }

    private void DesativarInimigo()
    {
        NavMeshAgent agent = inimigo.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.ResetPath();
            agent.Warp(inimigoPosicaoInicial);
        }
        else
        {
            inimigo.transform.position = inimigoPosicaoInicial;
        }

        inimigo.SetActive(false);

        alertLevel = 0;
        tempoForaDoFOV = 0f;
        alertStage = AlertStage.Curioso;
    }
}
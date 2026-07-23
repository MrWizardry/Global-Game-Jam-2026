using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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

    private EnemyLimiter limiter;

    private void Awake()
    {
        alertStage = AlertStage.Curioso;
        alertLevel = 0;

        inimigoPosicaoInicial = inimigo.transform.position;
        inimigo.SetActive(false);

        limiter = FindAnyObjectByType<EnemyLimiter>();
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

                
                if (limiter != null && limiter.alertActive && alertStage != AlertStage.Alerta)
                {
                    alertLevel = 0;
                    return;
                }

                if (playerInFOV)
                {
                    alertLevel += Time.deltaTime * 60f;
                    tempoForaDoFOV = 0f;

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

                
                if (!inimigo.activeInHierarchy && limiter != null && limiter.CanActivate())
                {
                    inimigo.SetActive(true);
                    limiter.alertActive = true;
                }

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

        if (limiter != null)
            limiter.alertActive = false;

        alertLevel = 0;
        tempoForaDoFOV = 0f;
        alertStage = AlertStage.Curioso;
    }
}
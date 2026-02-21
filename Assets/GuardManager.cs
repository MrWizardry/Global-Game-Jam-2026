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
    public float tempoAtivoInimigo = 10;
    private Vector3 inimigoPosicaoInicial;
    private Coroutine inimigoCoroutine;

    private void Awake()
    {
        alertStage = AlertStage.Curioso;
        alertLevel = 0;

        inimigoPosicaoInicial = inimigo.transform.position;
        inimigo.SetActive(false);
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
                if (!playerInFOV) 
                {
                    if(inimigoCoroutine == null)
                    {
                        inimigoCoroutine = StartCoroutine(AtivarInimigoPorTempo());
                    }
                           
                }
                    
                break;
        }
    }

    private IEnumerator AtivarInimigoPorTempo()
    {
        

        NavMeshAgent agent = inimigo.GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.ResetPath();

        yield return new WaitForSeconds(tempoAtivoInimigo);

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
        alertStage = AlertStage.Curioso;

        inimigoCoroutine = null;
    }
}

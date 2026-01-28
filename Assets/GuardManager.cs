using UnityEngine;

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
    [Range(0, 400)] public float alertLevel;

    public GameObject inimigo;

    private void Awake()
    {
        alertStage = AlertStage.Curioso;
        alertLevel = 0;
        inimigo.SetActive(false);  
    }

    private void Update()
    {
        bool playerInFOV = false;
        Collider[] targertsInFOV = Physics.OverlapSphere(transform.position, fov);
        foreach(Collider collider in targertsInFOV)
        {
            if (collider.CompareTag("Player"))
            {
                float signedAngle = Vector3.Angle(
                    transform.forward,
                    collider.transform.position - transform.position);
                if (Mathf.Abs(signedAngle) < fovAngle / 2)
                playerInFOV = true;
                break;
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
                    alertLevel++;
                    if (alertLevel >= 400)
                        alertStage = AlertStage.Alerta;
                }
                else
                {
                    alertLevel--;
                    if(alertLevel <= 0)
                        alertStage = AlertStage.Curioso;
                }
                break;
            case AlertStage.Alerta:
                inimigo.SetActive(true);
                if (!playerInFOV)
                    alertStage = AlertStage.Investigando;
                break;
        }
    }
}

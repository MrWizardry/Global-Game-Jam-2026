using UnityEngine;
using UnityEngine.InputSystem;

public class MissaooMulher : MonoBehaviour
{
    public bool procurarAlianca;
    public bool encontreiAlianca;
    public bool devolviAlianca;
    public bool missaoMulherCompleta;
    public bool missaoMulherIniciada;
    public GameObject alianca;
    public UIReferenceToQuest uIReferenceToQuest;


    private void Start()
    {
        missaoMulherIniciada = false;
        missaoMulherCompleta = false;
        procurarAlianca = false;
        encontreiAlianca = false;   
        devolviAlianca = false;
        alianca.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            missaoMulherIniciada = true;
            if (other.CompareTag("Mulher") && !procurarAlianca)
            {
                uIReferenceToQuest.ActivateWomanMissionUI();
                procurarAlianca = true;
                alianca.SetActive(true);
                Debug.Log("Procure a Alianca perdida");
                return;
            }

            
            if (encontreiAlianca && other.CompareTag("Mulher"))
            {
                devolviAlianca = true;
                missaoMulherCompleta = true;
                Debug.Log("Devolvi Alianca");
                missaoMulherCompleta = true;
                uIReferenceToQuest.DeactivateWomanMissionUI();
                return;
            }

            // ENCONTRAR ALIANÇA
            if (procurarAlianca)
            {
                if (other.CompareTag("Aliança") && !encontreiAlianca)
                {
                    encontreiAlianca = true;
                    Debug.Log("Encontrou Aliança. Devolva para Mulher");
                    alianca.SetActive(false);
                    return;
                }

                if (other.CompareTag("Mulher") && !encontreiAlianca)
                {
                    Debug.Log("Por favor, encontre minha aliança");
                    return;
                }
            }
        }
        
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class MissaooMulher : MonoBehaviour
{
    public bool procurarAlianca;
    public bool encontreiAlianca;
    public bool devolviAlianca;
    public bool missaoMulherCompleta;
    public GameObject alianca;


    private void Start()
    {
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
            
            if (other.CompareTag("Mulher") && !procurarAlianca)
            {
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

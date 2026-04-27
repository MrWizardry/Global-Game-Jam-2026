using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CozinheiroMissao : MonoBehaviour
{
    public MissaooMulher missaooMulher;
    //Coleta
    public bool peguei4Tacas;
    //Entrega
    public bool entregueiTaca1;
    public bool entregueiTaca2;
    public bool entregueiTaca3;
    public bool entregueiTaca4;
    public bool missaoCozinheiroCompleta;
    public bool missaoCozinheiroIniciada;
    public UIReferenceToQuest uIReferenceToQuest;

    private void Start()
    {
       peguei4Tacas = false;
       missaoCozinheiroCompleta = false;
       missaoCozinheiroIniciada = false;

    }

    void OnTriggerStay(Collider other)
    {
        if (Keyboard.current == null) return;
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame && missaooMulher.devolviAlianca == true)
            {
                if (other.CompareTag("Cozinheiro") && !peguei4Tacas)
                {
                    missaoCozinheiroIniciada = true;
                    uIReferenceToQuest.ActivateCookMissionUI();
                    peguei4Tacas = true;
                    Debug.Log("Pegou as taças! Entregue aos NPCs.");
                    return;
                }

                if (peguei4Tacas)
                {
                    if (other.CompareTag("NPC1") && !entregueiTaca1)
                    {
                        entregueiTaca1 = true;
                        Debug.Log("Entregou para NPC 1");
                    }
                    else if (other.CompareTag("NPC2") && !entregueiTaca2)
                    {
                        entregueiTaca2 = true;
                        Debug.Log("Entregou para NPC 2");
                    }
                    else if (other.CompareTag("NPC3") && !entregueiTaca3)
                    {
                        entregueiTaca3 = true;
                        Debug.Log("Entregou para NPC 3");
                    }
                    else if (other.CompareTag("NPC4") && !entregueiTaca4)
                    {
                        entregueiTaca4 = true;
                        Debug.Log("Entregou para NPC 4");
                    }

                    if (entregueiTaca1 && entregueiTaca2 && entregueiTaca3 && entregueiTaca4)
                    {
                        missaoCozinheiroCompleta = true;
                        Debug.Log("MISSÃO COMPLETA");
                        uIReferenceToQuest.DeactivateCookMissionUI();
                    }
                }
                else
                {
                    Debug.Log("Você ainda no pegou as taças");
                }
            }
        }
        
    }
}

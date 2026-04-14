using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CozinheiroMissao : MonoBehaviour
{
    //Coleta
    public bool peguei4Taças;
    //Entrega
    public bool entregueiTaça1;
    public bool entregueiTaça2;
    public bool entregueiTaça3;
    public bool entregueiTaça4;
    public bool missaoCompleta;
    public bool missaoIniciada;

    private void Start()
    {
       peguei4Taças = false;
       missaoCompleta = false;
       missaoIniciada = false;

    }

    void OnTriggerStay(Collider other)
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (other.CompareTag("Cozinheiro") && !peguei4Taças)
            {
                missaoIniciada= true;
                peguei4Taças = true;
                Debug.Log("Pegou as taças! Entregue aos NPCs.");
                return;
            }

            if (peguei4Taças)
            {
                if (other.CompareTag("NPC1") && !entregueiTaça1)
                {
                    entregueiTaça1 = true;
                    Debug.Log("Entregou para NPC 1");
                }
                else if (other.CompareTag("NPC2") && !entregueiTaça2)
                {
                    entregueiTaça2 = true;
                    Debug.Log("Entregou para NPC 2");
                }
                else if (other.CompareTag("NPC3") && !entregueiTaça3)
                {
                    entregueiTaça3 = true;
                    Debug.Log("Entregou para NPC 3");
                }
                else if (other.CompareTag("NPC4") && !entregueiTaça4)
                {
                    entregueiTaça4 = true;
                    Debug.Log("Entregou para NPC 4");
                }

                if (entregueiTaça1 && entregueiTaça2 && entregueiTaça3 && entregueiTaça4)
                {
                    missaoCompleta = true;
                    Debug.Log("MISSÃO COMPLETA");
                }
            }
            else
            {
                Debug.Log("Você ainda não pegou as taças");
            }
        }
    }
}

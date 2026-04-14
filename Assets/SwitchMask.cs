using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class SwitchMask : MonoBehaviour
{
    public GameObject mascaraDeConvidado;
    public GameObject mascaraDeFuncionario;
    public GameObject mascaraDeGerente;
    public bool missaoPaiConcluida;
    public bool missaoMulherConcluida;
    public bool missaoHomemConcluida;

    private void Start()
    {
        mascaraDeConvidado.SetActive(false);
        mascaraDeFuncionario.SetActive(false);
        mascaraDeGerente.SetActive(false);
        missaoPaiConcluida = false;
        missaoMulherConcluida = false;
        missaoHomemConcluida = false;
    }

    private void Update()
    {
        if (missaoHomemConcluida)
        {
            AtivarMascara(mascaraDeGerente);
        }
        else if (missaoMulherConcluida)
        {
            AtivarMascara(mascaraDeFuncionario);
        }
        else if (missaoPaiConcluida)
        {
            AtivarMascara(mascaraDeConvidado);
        }
    }

    void AtivarMascara(GameObject mascara)
    {
        mascaraDeConvidado.SetActive(false);
        mascaraDeFuncionario.SetActive(false);
        mascaraDeGerente.SetActive(false);

        mascara.SetActive(true);
    }

}


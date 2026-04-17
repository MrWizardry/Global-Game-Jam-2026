using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
public class SwitchMask : MonoBehaviour
{
    public GameObject mascaraDeConvidado;
    public GameObject mascaraDeFuncionario;
    public GameObject mascaraDeGerente;
    //public bool missaoPaiConcluida;
    MissaooMulher missaooMulher;
    CozinheiroMissao cozinheiroMissao;
    

    private void Start()
    {

        missaooMulher = GetComponent<MissaooMulher>();
        cozinheiroMissao = GetComponent<CozinheiroMissao>();

        mascaraDeConvidado.SetActive(true);
        mascaraDeFuncionario.SetActive(false);
        mascaraDeGerente.SetActive(false);
        //missaoPaiConcluida = false;
        
    }

    private void Update()
    {
        if (cozinheiroMissao.missaoCozinheiroCompleta)
        {
            AtivarMascara(mascaraDeGerente);
        }
        else if (missaooMulher.missaoMulherCompleta)
        {
            AtivarMascara(mascaraDeFuncionario);
        }
        //else if (missaoPaiConcluida)
        //{
            //AtivarMascara(mascaraDeConvidado);
        //}
    }

    void AtivarMascara(GameObject mascara)
    {
        //mascaraDeConvidado.SetActive(false);
        mascaraDeFuncionario.SetActive(false);
        mascaraDeGerente.SetActive(false);

        mascara.SetActive(true);
    }

}


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
    public GameObject portaZeladoria;
    public GameObject portaCozinhaUm;
    public GameObject portaCozinhaDois;
    public GameObject EntradaElevador;


    private void Start()
    {

        missaooMulher = GetComponent<MissaooMulher>();
        cozinheiroMissao = GetComponent<CozinheiroMissao>();

        portaZeladoria.SetActive(true);
        portaCozinhaUm.SetActive(true);
        portaCozinhaDois.SetActive(true);
        EntradaElevador.SetActive(true);
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
            EntradaElevador.SetActive(false);
        }
        else if (missaooMulher.missaoMulherCompleta)
        {
            AtivarMascara(mascaraDeFuncionario);
            portaZeladoria.SetActive(false);
            portaCozinhaUm.SetActive(false);
            portaCozinhaDois.SetActive(false);
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


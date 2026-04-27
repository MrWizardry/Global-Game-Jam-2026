using UnityEngine;
using UnityEngine.UI;

public class UIReferenceToQuest : MonoBehaviour
{
    public CozinheiroMissao cozinheiroMissao;
    public MissaooMulher missaooMulher;

    [Header("Mulher")]
    public GameObject objectivesMulher;
    public Toggle Obj1, Obj2, Obj3;

    [Header("Cozinheiro")]
    public GameObject objectivesCozinheiro;
    public Toggle Objt1, Objt2, Objt3;

    private void Update()
    {
        if (missaooMulher.missaoMulherIniciada)
        {
            Obj1.isOn = missaooMulher.procurarAlianca;
            Obj2.isOn = missaooMulher.encontreiAlianca;
            Obj3.isOn = missaooMulher.missaoMulherCompleta;
        }

        if (cozinheiroMissao.missaoCozinheiroIniciada)
        {
            Objt1.isOn = cozinheiroMissao.peguei4Tacas;
            Objt2.isOn =
                cozinheiroMissao.entregueiTaca1 &&
                cozinheiroMissao.entregueiTaca2 &&
                cozinheiroMissao.entregueiTaca3 &&
                cozinheiroMissao.entregueiTaca4;

            Objt3.isOn = cozinheiroMissao.missaoCozinheiroCompleta;
        }
    }

    public void ActivateWomanMissionUI()
    {
        objectivesMulher.SetActive(true);
    }

    public void ActivateCookMissionUI()
    {
        objectivesCozinheiro.SetActive(true);
    }

    public void DeactivateCookMissionUI()
    {
        objectivesCozinheiro.SetActive(false);
    }
    public void DeactivateWomanMissionUI()
    {
        objectivesMulher.SetActive(false);
    }
}
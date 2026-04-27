using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueData dialogoPrimeiraVez;
    public DialogueData dialogoDuranteMissao;
    public DialogueData dialogoAposCompletar;

    [Header("Missões")]
    public MissaooMulher missaoMulher;
    public CozinheiroMissao missaoCozinheiro;


    public DialogueData GetDialogue()
    {
        // 👩 Mulher
        if (missaoMulher != null)
        {
            if (missaoMulher.missaoMulherCompleta)
                return dialogoAposCompletar;

            if (missaoMulher.missaoMulherIniciada)
                return dialogoDuranteMissao;

            return dialogoPrimeiraVez;
        }

        // 🍳 Cozinheiro
        if (missaoCozinheiro != null)
        {
            if (missaoCozinheiro.missaoCozinheiroCompleta)
                return dialogoAposCompletar;

            if (missaoCozinheiro.missaoCozinheiroIniciada)
                return dialogoDuranteMissao;

            return dialogoPrimeiraVez;
        }

        return dialogoPrimeiraVez;
    }
}

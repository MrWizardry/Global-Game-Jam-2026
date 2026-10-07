using UnityEngine;

public class VerificadorLivros : MonoBehaviour
{
    public LivroPosicao[] posicoes;
    public bool todosCorretos;

    public AudioSource audioSource;
    public AudioClip somVitoria;

    private bool jaTocou = false;

    void Update()
    {
        todosCorretos = true;

        foreach (LivroPosicao posicao in posicoes)
        {
            if (!posicao.correto)
            {
                todosCorretos = false;
                break;
            }
        }

        if (todosCorretos && !jaTocou)
        {
            audioSource.PlayOneShot(somVitoria);
            jaTocou = true;
            
        }
    }
}

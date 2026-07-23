using UnityEngine;

public class BookSelector : MonoBehaviour
{
    private static GameObject livroSelecionado;
    public VerificadorLivros VerificadorLivros;
    private Collider meuColisor;

    private void Start()
    {
        meuColisor = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        if (livroSelecionado == null)
        {
            livroSelecionado = gameObject;
        }
        else if (livroSelecionado != gameObject)
        {
            Vector3 posicaoTemp = livroSelecionado.transform.position;

            livroSelecionado.transform.position = transform.position;
            transform.position = posicaoTemp;

            livroSelecionado = null;
        }
        else
        {
            livroSelecionado = null;
        }
    }

    private void Update()
    {
        if(VerificadorLivros.todosCorretos == true)
        {
            meuColisor.enabled = false;
        }
    }

}
using UnityEngine;

public class LivroPosicao : MonoBehaviour
{
    public string nome;
    public bool correto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == nome)
        {
            correto = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == nome)
        {
            correto = false;
        }
    }
}
